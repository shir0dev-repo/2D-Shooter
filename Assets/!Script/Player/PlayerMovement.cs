using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : Movement
{
    [SerializeField] private float _decelerationForce = 20f;
    [SerializeField] private PlayerInputHandler _playerInputHandler;

    protected override void Awake()
    {
        base.Awake();

        if (_playerInputHandler == null)
            _playerInputHandler = GetComponent<PlayerInputHandler>();
    }

    protected override void HandleMovement()
    {
        float inputDirection = _playerInputHandler.MoveAction.ReadValue<float>();

        Vector3 horizontalVelocity = _rigidbody.velocity;
        horizontalVelocity.y = 0;


        //DECEL CODE
        if (inputDirection == 0)
        {
            if (horizontalVelocity.sqrMagnitude <= 0.5f)
                _rigidbody.velocity = new Vector3(0, _rigidbody.velocity.y + (Physics2D.gravity.y * Time.fixedDeltaTime));

            //Has velocity, but is NOT inputting movement
            else //:D
            {
                Vector2 decelerationDirection = -horizontalVelocity.normalized * _decelerationForce * Time.fixedDeltaTime;
                _rigidbody.AddForce(decelerationDirection, ForceMode2D.Impulse);
            }
        }

        //MOVE CODE -> inputDirection != 0
        else
        {
            float facingDirection = inputDirection > 0 ? 0 : 180f;
            transform.rotation = Quaternion.Euler(0, facingDirection, 0);

            float rigidbodyVerticalVelocity = _rigidbody.velocity.y;

            if (horizontalVelocity.sqrMagnitude >= _maxSpeed * _maxSpeed)
            {
                _rigidbody.velocity = new Vector3(inputDirection * _maxSpeed, rigidbodyVerticalVelocity);
            }
            else
            {
                Vector3 moveDirection = _moveSpeed * Time.fixedDeltaTime * new Vector3(inputDirection, 0f);
                _rigidbody.AddForce(moveDirection, ForceMode2D.Impulse);
            }
        }
    }
}