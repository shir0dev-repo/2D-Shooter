using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))] //OPTIONAL: When Script Requires Component, will automatically add said component when adding this script.
public class PlayerInputHandler : MonoBehaviour
{
    private const string _MOVE_ACTION_NAME = "Move";
    private const string _ATTACK_ACTION_NAME = "Attack";
    private const string _JUMP_ACTION_NAME = "Jump";

    [SerializeField] private float _playerMoveSpeed = 8f, _playerMaxMoveSpeed = 13f;
    [SerializeField] private float _jumpForce = 0.5f;
    [SerializeField] private Rigidbody2D _rigidbody;

    private PlayerInputActionsAsset _playerActionsAsset;
    private InputAction _moveAction;
    private InputAction _jumpAction;
    private InputAction _attackAction;

    private void Awake()
    {
        _playerActionsAsset = new PlayerInputActionsAsset(); //Create new "instance" or "copy" of ActionsAsset

        if (_rigidbody == null)
            _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        _playerActionsAsset.Enable();
        _moveAction = _playerActionsAsset.FindAction(_MOVE_ACTION_NAME); //Get reference to specific action from the NAME IN ACTION ASSET IN UNITY
        _jumpAction = _playerActionsAsset.FindAction(_JUMP_ACTION_NAME);
        _jumpAction.started += HandleJump;
    }

    private void FixedUpdate()
    {
        //If frame 1 is at 12:00pm, and frame 2 is at 12:01pm, Time.deltaTime = 1 minute.
        //Speeding up to 60fps, that means every frame is rendered 1/60 seconds, causing Time.deltaTime = 1/60 seconds.
        //float t = Time.deltaTime;
        HandleMovement();

        //use _jumpAction.started += JumpFunction;
        //NOT in Update();
        //Adding to transform.position.y;
    }

    private void HandleJump(InputAction.CallbackContext context)
    {
        _rigidbody.AddForce(new Vector2(0, _jumpForce), ForceMode2D.Impulse);
    }

    private void OnDisable()
    {
        _playerActionsAsset.Disable();
        _jumpAction.started -= HandleJump;
    }

    private void HandleMovement()
    {
        //Check if player is pressed WASD.
        //S and W are not needed, because sidescroller. To fix this,
        // the Move Action is no long UP/DOWN/LEFT/RIGHT, but now LEFT/RIGHT (Axis instead of Vector2 value)
        // Vector2 inputDirectionVector = _moveAction.ReadValue<Vector2>(); 
        // float inputDirection = inputDirection.x;
        //InputAction -> ActionType = Value, ControlType = Axis.
        float inputDirection = _moveAction.ReadValue<float>(); //CONVERTED VECTOR 2 TO FLOAT: Since we aren't moving up and down, we only need the X component (A and D) in the Input Action (WASD).

        //STEPBYSTEP:
        /*
            grab COPY of rigidbody2D's velocity.
            store yValue (gravity or jump, doesnt matter) and save it for later.
            Take the X-only vector3, and check its MAGNITUDE (length/speed) against a maxValue.
            If you DONT take out y-value, magnitude with X AND Y could be greater than max speed without necessarily reaching that speed (thus eliminating acceleration/wind-up (THIS WOULD BE AN EDGE CASE)).
         */

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
                Vector3 moveDirection = _playerMoveSpeed * Time.deltaTime * new Vector3(inputDirection, 0f).normalized;
                _rigidbody.AddForce(moveDirection, ForceMode2D.Impulse); //ForceMode2D.Force for smoother movement (continuous force), and ForceMode2D.Impulse for jerkier/instant forces (jumping or knockback).
            }
        }
        else
        {
            //ADD FASTER DECELERATION
        }
    }
}

//V = d/t, acceleration = (velocityNow - velocityLastFrame) / (timeNow - timeLastFrame) == (deltaVelocity / deltaTime)

//Magnitude in this case really only means if the player is pressing WASD. magnitude == 0 if WASD is NOT pressed.
//Square roots are slow in computing, so grabbing the SQUARED magnitude is a slight optimization.
/*        if (inputMoveVector.sqrMagnitude > 0.01f)
        {
            moveDirection = new Vector3(inputMoveVector.x, inputMoveVector.y);
            transform.position += moveDirection * Time.deltaTime * _playerMoveSpeed; //realistically, this is ALL you need to move something in unity. (in update method) teehee
        }*/

/*
    ***Moving 1 unit (normalized vector value) every frame. @60FPS, 60u/second.
transform.position += moveDirection;

    ***Multiplying by Time.deltaTime brings it "back within range" so instead of moving 60units/second, you move 1unit/second, effectively "normalizing" the rate of speed.
transform.position += moveDirection * Time.deltaTime;

    ***Finally, by adding in our OWN variable (not deltaTime or inputValue), we can fully control how fast the player can move.
*/

/*
private void FixedUpdate()
{
    //This is a fixed timestep that updates every 0.02seconds (by default, is changeable). Should only be used inside fixed update, because fixed update is CALLED every Time.fixedDeltaTime seconds.
    //float t = Time.fixedDeltaTime;
}
*/


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

//This is a github push test
//This is secendary testttt :3