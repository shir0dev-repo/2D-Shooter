using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour, IDamageable
{
    [Header("Movement")]
    [SerializeField] private float _playerMoveSpeed = 16f;
    [SerializeField] private float _playerMaxMoveSpeed = 13f;
    [SerializeField] private float _decelerationForce = 20f;

    [Header("References")]
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private PlayerInputHandler _playerInputHandler;


    public void TakeDamage()
    {
        GameManager.Instance.OnPlayerDeath?.Invoke();
        Destroy(gameObject);
    }


    private void Awake()
    {
        if (_rigidbody == null)
            _rigidbody = GetComponent<Rigidbody2D>();

        if (_playerInputHandler == null)
            _playerInputHandler = GetComponent<PlayerInputHandler>();
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    private void HandleMovement()
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

            if (horizontalVelocity.sqrMagnitude >= _playerMaxMoveSpeed * _playerMaxMoveSpeed)
            {
                _rigidbody.velocity = new Vector3(inputDirection * _playerMaxMoveSpeed, rigidbodyVerticalVelocity);
            }
            else
            {
                Vector3 moveDirection = _playerMoveSpeed * Time.fixedDeltaTime * new Vector3(inputDirection, 0f);
                _rigidbody.AddForce(moveDirection, ForceMode2D.Impulse);
            }
        }
    }
}