using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] float _speed = 3.0f;
    private Vector3 _moveVec = Vector3.zero;

    private bool canJump = true;

    private void Update()
    {
        transform.Translate(_speed * Time.deltaTime * _moveVec);
        //_rigidbody.AddForce(_speed * Time.deltaTime * _moveVec);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        //transform.Translate(context.ReadValue<Vector2>() * _speed * Time.deltaTime);
        //_rigidbody.AddForce(context.ReadValue<Vector2>() * _speed);
        Vector3 inputVec = context.ReadValue<Vector2>();
        _moveVec = new Vector3(inputVec.x, inputVec.y, 0);
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed && canJump)
        {
            _rigidbody.AddForce(Vector2.up * 10, ForceMode2D.Impulse);
            canJump = false;
        }
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        Debug.Log("Fire!");
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Platform"))
        {
            Debug.Log("Collided");
            canJump = true;
        }
    }
}
