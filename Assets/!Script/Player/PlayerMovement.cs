using UnityEngine;
using UnityEngine.InputSystem;

//OPTIONAL: When Script Requires Component, will automatically add said component when adding this script.
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _playerMoveSpeed = 16f, _playerMaxMoveSpeed = 13f;
    [SerializeField] private float _jumpForce = 7f;
    [SerializeField] private Rigidbody2D _rigidbody;

    [SerializeField] private PlayerInputHandler _playerInputHandler;

    bool grounded;
    private float _decelerationForce = 20f;

    private void Awake()
    {
        //If the Rigidbody2D is NOT SET IN THE INSPECTOR, script will grab it for you.
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
        if (grounded == true)
        {
            _rigidbody.AddForce(new Vector2(0, _jumpForce), ForceMode2D.Impulse);
        }
    }

    private void HandleMovement()
    {
        //CONVERTED VECTOR 2 TO FLOAT: Since we aren't moving up and down, we only need the X component (A and D) in the Input Action (WASD).
        float inputDirection = _playerInputHandler.MoveAction.ReadValue<float>(); 

        if (inputDirection != 0)
        {
            Vector3 rigidbodyHorizontalVelocity = _rigidbody.velocity;

            float rigidbodyVerticalVelocity = _rigidbody.velocity.y;

            rigidbodyHorizontalVelocity.y = 0;

            if (rigidbodyHorizontalVelocity.sqrMagnitude >= _playerMaxMoveSpeed * _playerMaxMoveSpeed)
            {
                Debug.Log(inputDirection);
                _rigidbody.velocity = new Vector3(inputDirection * _playerMaxMoveSpeed, rigidbodyVerticalVelocity);
                Debug.Log("Capped Speed! " + _rigidbody.velocity);
            }
            else
            {
                Vector3 moveDirection = _playerMoveSpeed * Time.fixedDeltaTime * new Vector3(inputDirection, 0f).normalized;
                _rigidbody.AddForce(moveDirection, ForceMode2D.Impulse); //ForceMode2D.Force for smoother movement (continuous force), and ForceMode2D.Impulse for jerkier/instant forces (jumping or knockback).
            }
        }
        else
        {
            //ADD FASTER DECELERATION...Something like this? :D :D
            if ((_rigidbody.velocity.x > 0.1f || _rigidbody.velocity.x < -0.1f))
            {
                Vector2 horizontalVelocity = _rigidbody.velocity;
                horizontalVelocity.y = 0f;
                Vector2 decelerationDirection = -horizontalVelocity.normalized * _decelerationForce * Time.fixedDeltaTime;
                _rigidbody.AddForce(decelerationDirection, ForceMode2D.Impulse);
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D other) 
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            grounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            grounded = false;
        }
    }
}

/*
private void HandleMovement(InputAction.CallbackContext context)
{
    //If holding D key, x = 1 and y = 0. If holding A key, x = -1, y = 0. If holding W key, x = 0, y = 1. If holding S key, x = 0, y = -1.
    //If holding W and D keys, x = 1, y = 1, actual vector size is a bit bigger than 1.

    //The "Control Type" of the Action inside ActionsAsset. Normalized is readjusting the Vector2 to the range -1 and 1.
    Vector2 inputMoveVector = context.ReadValue<Vector2>().normalized; 


    _playerTransform.position += new Vector3(inputMoveVector.x, inputMoveVector.y);
}
*/

/*
    step-by-step of movement calculations:

    grab COPY of rigidbody2D's velocity.
    store yValue (gravity or jump, doesnt matter) and save it for later.
    Take the X-only vector3, and check its MAGNITUDE (length/speed) against a maxValue.
    If you DONT take out y-value, magnitude with X AND Y could be greater than max speed without necessarily reaching that speed (thus eliminating acceleration/wind-up (THIS WOULD BE AN EDGE CASE)).
*/