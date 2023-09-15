using System.Numerics;
using UnityEngine;
using UnityEngine.InputSystem;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] Animator _animator;
    [SerializeField] InventoryManager _inventoryManager;
    [SerializeField] EnemyManager _enemyManager;

    private Transform hitSpot;

    public float _speed;

    public float _jumpHeight = 10;

    private Vector3 _moveVec = Vector3.zero;

    private bool canJump = true;
    public bool isSolvingPuzzle = false;
    private bool isAlive = true;

    private GameObject currentPlatform;

    public AudioClip runningSound;
    public AudioClip jumpingSound;
    private AudioSource audioSource;

    private RaycastHit2D attackRay;

    private float attackTimer = 0f;

    private void Start()
    {
        // fatch initial player stats values
        _speed = gameObject.GetComponent<PlayerStats>().GetSpeed();

        // Initialize the audio source
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = runningSound;
        audioSource.loop = true; // So the sound keeps playing while the player is moving

        // Get the player hitspot
        hitSpot = transform.GetChild(1).GetComponent<Transform>();

        _animator.SetBool("weaponEquipped", false);
    }

    private void FixedUpdate()
    {
        if (currentPlatform != null)
        {
            transform.Translate(currentPlatform.GetComponent<PlatformMove>().movement);
        }

        if (isAlive)
        {
            transform.Translate(_speed * Time.deltaTime * _moveVec);
        }

        if (attackTimer > 0f)
        {
            attackTimer -= Time.deltaTime;
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        CheckDirection(context);

        _animator.SetBool("isRunning", true);
        Vector3 inputVec = context.ReadValue<Vector2>();
        _moveVec = new Vector3(inputVec.x, inputVec.y, 0);

        // Play the running sound
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
            //Debug.Log("Running Audio Playing");
        }

        if (context.canceled)
        {
            _animator.SetBool("isRunning", false);

            // Stop the running sound
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }
    }
    // Function checks running direction of player and flips the sprite on the x axis if necessary
    public void CheckDirection(InputAction.CallbackContext context)
    {

        if (context.ReadValue<Vector2>().x == 1 && gameObject.transform.localScale.x == 1)
        {
            gameObject.transform.localScale *= new Vector2(-1f,1f);
            //gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (context.ReadValue<Vector2>().x == -1 && gameObject.transform.localScale.x == -1)
        {
            gameObject.transform.localScale *= new Vector2(-1f,1f);
            //gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed && canJump)
        {
            _animator.SetTrigger("isJump");
            _rigidbody.AddForce(Vector2.up * _jumpHeight, ForceMode2D.Impulse);
            canJump = false;

            // Play the jumping sound
            audioSource.PlayOneShot(jumpingSound);
        }
    }

    public void ItemEquipped()
    {
        transform.Find("EquipSlot").GetComponentInChildren<Transform>().position = Vector2.zero;
        _animator.SetTrigger("weaponEquipped");
    }

    public void ItemUnequipped()
    {
        _animator.SetTrigger("weaponUnequipped");
    }

    public void UseItem(InputAction.CallbackContext context)
    {
        if (context.performed && !isSolvingPuzzle)
        {
            Debug.Log("Use item method running");
            // Get selected item
            if (_inventoryManager.itemEquipped)
            {
                InventoryItem selectedItem = _inventoryManager.GetSelectedItem();
                if (selectedItem.item.type == ItemType.Powerup)
                {
                    UsePowerup(selectedItem);
                }
                else if (selectedItem.item.type == ItemType.Weapon)
                {
                    UseWeapon(selectedItem);
                }
            } else
            {
                Debug.Log("no item to use");
                return;
            }
        }
    }

    private void UsePowerup(InventoryItem selectedItem)
    {
        // Powerup code

        //Debug.Log("using consumable : " + selectedItem.item);
        gameObject.GetComponent<PlayerStats>().ProcessBuff(selectedItem.item);
        selectedItem.count--;
        //Debug.Log("Consumable count remaining: " + selectedItem.count);
        selectedItem.RefreshCount();
        if (selectedItem.count <= 0)
        {
            Destroy(selectedItem.gameObject);
            _inventoryManager.UnequipItem();
            _inventoryManager.DeselectCurrent();
        }
    }

    private void UseWeapon(InventoryItem selectedItem)
    {
        if (attackTimer < 1f)
        {
            // Weapon code
            _animator.SetTrigger("isAttack");
            for (int i = 0; i < _enemyManager.enemies.Count; i++)
            {
                if (_enemyManager.enemies[i] != null)
                {
                    float distance = Vector3.Distance(hitSpot.position, _enemyManager.enemies[i].transform.position);

                    // Check if enemy is in clear sight
                    if (distance < selectedItem.item.actionRange && IsEnemyVisible(i, selectedItem.item.actionRange))
                    {
                        _enemyManager.enemies[i].transform.Find("EnemyBody").GetComponent<EnemyInfo>().TakeDamage(selectedItem.item.actionValue);
                    }
                }
            }
            attackTimer = 1.5f;
        }
    }

    // Must still fix this
    private bool IsEnemyVisible(int enemyIndex, float range)
    {
        // Get player Hitspot
        Transform enemyTrans = _enemyManager.enemies[enemyIndex].transform;

        // Get direction of enemy as unit vector
        Vector3 enemyDir = (enemyTrans.position - hitSpot.position).normalized;

        // Get mask for player
        int playerMask = LayerMask.GetMask("Player");

        // Get mask for pickups
        int pickupLayerMask = LayerMask.GetMask("Pickups");

        // Get mask for enemy to avoid hitting undesired colliders
        int enemyLayerMask = LayerMask.GetMask("Enemy");

        // Combine the two masks
        int combinedLayerMask = playerMask | pickupLayerMask | enemyLayerMask;

        // Cast the ray from player,
        // in direction of enemy,
        // distance of weapon range,
        // negating the combined mask to ignore player and pickups

        attackRay = Physics2D.Raycast(hitSpot.position, enemyDir, Mathf.Infinity, ~combinedLayerMask);
        //Debug.DrawRay(hitSpot.position, enemyDir * range, Color.green);

        if (attackRay.collider != null)
        {
            if (attackRay.collider.gameObject.CompareTag("EnemyHitSpot"))
            {
                return true;
            }
        }
        return false;
    }

    public void PlayerDie()
    {
        isAlive = false;
        _animator.SetTrigger("isDead");
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        // collision with platforms
        if (col.gameObject.CompareTag("Platform"))
        {
            if (col.gameObject.GetComponent<PlatformMove>() != null)
            {
                currentPlatform = col.gameObject;
            } else
            {
                currentPlatform = null;
            }
            canJump = true;
            _animator.ResetTrigger("isJump");
        }
    }
    
    private void OnCollisionExit2D(Collision2D col)
    {
        // collision with platforms
        if (col.gameObject.CompareTag("Platform"))
        {
            currentPlatform = null;
        }
    }
}
