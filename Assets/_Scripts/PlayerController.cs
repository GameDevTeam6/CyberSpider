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
    public float _score;
    public float _health;
    public float _time;
    public float _jumpHeight = 10;

    private Vector3 _moveVec = Vector3.zero;

    private bool canJump = true;
    private bool isSolvingPuzzle = false;

    private GameObject currentPlatform;

    private void Start()
    {
        // fatch initial player stats values
        _speed = gameObject.GetComponent<PlayerStats>().GetSpeed();
        _health = gameObject.GetComponent<PlayerStats>().GetHealth();
        _score = gameObject.GetComponent<PlayerStats>().GetScore();
        _time = gameObject.GetComponent<PlayerStats>().GetTime();
    }

    private void Update()
    {

    }

    private void FixedUpdate()
    {
        if (currentPlatform != null)
        {
            transform.Translate(currentPlatform.GetComponent<PlatformMove>().movement);
        }

        transform.Translate(_speed * Time.deltaTime * _moveVec);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        CheckDirection(context);

        _animator.SetBool("isRunning", true);
        Vector3 inputVec = context.ReadValue<Vector2>();
        _moveVec = new Vector3(inputVec.x, inputVec.y, 0);

        if (context.canceled)
        {
            _animator.SetBool("isRunning", false);
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
        }
    }

    public void UseItem(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("Use item method running");
            // Get selected item
            InventoryItem selectedItem = _inventoryManager.GetSelectedItem();

            // Check that item is not null
            if (selectedItem != null)
            {
                if (selectedItem.item.type == ItemType.Powerup)
                {
                    UsePowerup(selectedItem);
                }
                else if (selectedItem.item.type == ItemType.Weapon)
                {
                    UseWeapon(selectedItem);
                }
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
            //Debug.Log("Supply of item finished, Destroying item");
            Destroy(selectedItem.gameObject);
            _inventoryManager.UnequipItem();
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

        // collision with pickup items
        if (col.gameObject.CompareTag("Pickup"))
        {
            // fetch type of item
            ActionType itemType = col.gameObject.GetComponent<ItemInfo>().item.actionType;

            ////////////////// BITCOIN TOKEN ///////////////////

            if (itemType == ActionType.Score)
            {
                // fetch reward value to be added to the score
                float val = col.gameObject.GetComponent<ItemInfo>().item.actionValue;
                // update score
                _score = gameObject.GetComponent<PlayerStats>().ChangeScore(val);
            }

            ////////////////// HEALTH TOKEN ///////////////////
            if (itemType == ActionType.Health)
            {
                // fetch current health
                float currentHealth = gameObject.GetComponent<PlayerStats>().GetHealth();
                // fetch health value to be added
                float val = col.gameObject.GetComponent<ItemInfo>().item.actionValue;

                if (currentHealth < (100 - val))
                {
                    // add boost value to current health
                    _health = gameObject.GetComponent<PlayerStats>().ChangeHealth(val);
                }
                else
                {
                    // restrict max health to 100
                    float diff = 100 - currentHealth;
                    _health = gameObject.GetComponent<PlayerStats>().ChangeHealth(diff);
                }
            }
        }
    }
}
