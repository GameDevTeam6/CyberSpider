using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] float _speed = 3.0f;
    [SerializeField] Animator _animator;
    private Vector3 _moveVec = Vector3.zero;
    public float _jumpHeight = 10;
    private bool canJump = true;
    [SerializeField] InventoryManager _inventoryManager;

    private void Update()
    {
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
        Debug.Log("Use item method running");
        //// Get selected item
        //InventoryItem selectedItem = _inventoryManager.GetSelectedItem();

        //// Check that item is not null
        //if (selectedItem != null)
        //{
        //    if (selectedItem.item.consumable)
        //    {
        //        Debug.Log("using consumable : " + selectedItem.item);
        //        selectedItem.count--;
        //        Debug.Log("Consumable count remaining: " + selectedItem.count);
        //        selectedItem.RefreshCount();
        //        if (selectedItem.count <= 0)
        //        {
        //            Debug.Log("Supply of item finished, Destroying item");
        //            Destroy(selectedItem.gameObject);
        //        }
        //    }
        //    else
        //    {
        //        // Implement non-consumable code
        //    }
        //}
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Platform"))
        {
            //Debug.Log("Collided");
            canJump = true;
            _animator.ResetTrigger("isJump");
        }
    }
}
