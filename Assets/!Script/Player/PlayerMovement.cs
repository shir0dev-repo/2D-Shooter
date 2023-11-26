using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour, IDamageable
{
    [Header("Movement")]
    [SerializeField] private float _playerMoveSpeed = 16f;
    [SerializeField] private float _playerMaxMoveSpeed = 13f;
    [SerializeField] private float _decelerationForce = 20f;

    [Header("Jump")]
    [SerializeField] private float _jumpForce = 7f;
    [SerializeField] private float _gravityMultiplier = 3.2f;
    [SerializeField] private float _maxVerticalVelocity = -20f;

    [Header("Ground Check")]
    [SerializeField] private LayerMask _groundCheckLayer;
    [SerializeField] private Vector2 _groundCheckSize;
    [SerializeField] private Vector2 _groundCheckPosition;

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

    private void Start()
    {
        _playerInputHandler.JumpAction.started += HandleJump;
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    private void OnDisable()
    {
        _playerInputHandler.JumpAction.started -= HandleJump;
    }
    private void HandleJump(InputAction.CallbackContext context)
    {
        if (IsGrounded())
        {
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
    }

    private void HandleMovement()
    {
        float inputDirection = _playerInputHandler.MoveAction.ReadValue<float>();

        HandleGravity();

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

    private void OnDrawGizmosSelected()
    {
        Color gCol = Color.yellow;
        gCol.a = 0.4f;
        Gizmos.color = gCol;

        Gizmos.DrawCube(transform.position + (Vector3)_groundCheckPosition, _groundCheckSize);
    }
}