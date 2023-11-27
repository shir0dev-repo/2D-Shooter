using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJump : MonoBehaviour
{
    [Header("Jump")]
    [SerializeField] private float _jumpForce = 7f;
    [SerializeField, Min(1f)] private float _gravityMultiplier = 3.2f;
    [SerializeField] private float _maxVerticalVelocity = -20f;

    [Header("Ground Check")]
    [SerializeField] private LayerMask _groundCheckLayer;
    [SerializeField] private Vector2 _groundCheckSize;
    [SerializeField] private Vector2 _groundCheckPosition;

    [Header("References")]
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private PlayerInputHandler _playerInputHandler;
    

    private bool _wasGroundedLastFrame = true;
    private bool _isGrounded = true;

    public static Action OnPlayerJump;
    public static Action OnPlayerLanded;
    public static Action<float> OnVerticalVelocityChanged;

    private void Awake()
    {
        if (_rigidbody == null)
            _rigidbody = GetComponent<Rigidbody2D>();

        if ( _playerInputHandler == null)
            _playerInputHandler = GetComponent<PlayerInputHandler>();
    }

    private void OnEnable()
    {
        _playerInputHandler.JumpAction.started += HandleJump;
    }

    private void FixedUpdate()
    {
        HandleGravity();
        
    }

    private void Update()
    {
        HandleGroundedState();
    }

    private void OnDisable()
    {
        _playerInputHandler.JumpAction.started -= HandleJump;
    }

    private void HandleGroundedState()
    {
        _isGrounded = IsGrounded();

        if (_isGrounded && !_wasGroundedLastFrame)
            OnPlayerLanded?.Invoke();

        _wasGroundedLastFrame = _isGrounded;
    }

    private void HandleGravity()
    {
        if (_rigidbody.velocity.y <= _maxVerticalVelocity)
        {
            _rigidbody.velocity = new(_rigidbody.velocity.x, _maxVerticalVelocity);

        }
        //Will not call elif when past _maxVerticalVelocity, but will call when between it and zero.
        else if (_rigidbody.velocity.y < 0f)
        {
            Vector3 gravityMultiplier = _gravityMultiplier * Time.fixedDeltaTime * Vector3.down;
            _rigidbody.AddForce(gravityMultiplier, ForceMode2D.Impulse);
        }
        OnVerticalVelocityChanged?.Invoke(_rigidbody.velocity.y);
    }

    private void HandleJump(InputAction.CallbackContext context)
    {
        if (IsGrounded())
        {
            OnPlayerJump?.Invoke();
            _rigidbody.AddForce(new Vector2(0, _jumpForce), ForceMode2D.Impulse);
        }
    }

    private bool IsGrounded()
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position + (Vector3)_groundCheckPosition, _groundCheckSize, 45f, _groundCheckLayer);

        foreach (Collider2D col in colliders)
        {
            if (((1 << col.gameObject.layer) & _groundCheckLayer) != 0)
                return true;
        }

        return false;
    }

    private void OnDrawGizmosSelected()
    {
        Color gCol = Color.yellow;
        gCol.a = 0.4f;
        Gizmos.color = gCol;

        Gizmos.DrawCube(transform.position + (Vector3)_groundCheckPosition, _groundCheckSize);
    }
}
