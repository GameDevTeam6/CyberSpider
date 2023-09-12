using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] Animator _animator;
    [SerializeField] InventoryManager _inventoryManager;
    [SerializeField] EnemyManager _enemyManager;

    public float _speed;

    public float _jumpHeight = 10;

    private Vector3 _moveVec = Vector3.zero;

    private bool canJump = true;
    private bool isSolvingPuzzle = false;
    private bool isAlive = true;

    private GameObject currentPlatform;

    public AudioClip runningSound;
    public AudioClip jumpingSound;
    private AudioSource audioSource;

    private void Start()
    {
        // fatch initial player stats values
        _speed = gameObject.GetComponent<PlayerStats>().GetSpeed();

        // Initialize the audio source
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = runningSound;
        audioSource.loop = true; // So the sound keeps playing while the player is moving
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
            Debug.Log("Running Audio Playing");
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
        if (context.ReadValue<Vector2>().x == 1)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
        else if (context.ReadValue<Vector2>().x == -1)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
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

    public void UseItem(InputAction.CallbackContext context)
    {
        if (context.performed)
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
        // Weapon code
        //_animator.SetBool("weaponEquipped", true);
        //_animator.SetTrigger("isAttack");
        for (int i = 0; i < _enemyManager.enemies.Count; i++)
        {
            if (_enemyManager.enemies[i] != null)
            {
                float distance = Vector3.Distance(transform.position, _enemyManager.enemies[i].transform.position);
                if (distance < selectedItem.item.actionRange)
                {
                    _enemyManager.enemies[i].transform.GetChild(0).GetComponent<EnemyInfo>().TakeDamage(selectedItem.item.actionValue);
                }
            }
        }
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
