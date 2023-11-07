using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    private const string _MOVE_ACTION_NAME = "Move";
    private const string _ATTACK_ACTION_NAME = "Attack";
    private const string _JUMP_ACTION_NAME = "Jump";

    [SerializeField] private float _playerSpeed = 8f;

    private PlayerInputActionsAsset _playerActionsAsset;
    private InputAction _moveAction;
    private InputAction _attackAction;
    private InputAction _jumpAction;

    private void Awake()
    {
        _playerActionsAsset = new PlayerInputActionsAsset(); //Create new "instance" or "copy" of ActionsAsset
    }

    private void OnEnable()
    {
        _playerActionsAsset.Enable();
        _moveAction = _playerActionsAsset.FindAction(_MOVE_ACTION_NAME); //Get reference to specific action from the NAME IN ACTION ASSET IN UNITY
        _jumpAction = _playerActionsAsset.FindAction(_JUMP_ACTION_NAME);
    }

    private void Update()
    {
        //If frame 1 is at 12:00pm, and frame 2 is at 12:01pm, Time.deltaTime = 1 minute.
        //Speeding up to 60fps, that means every frame is rendered 1/60 seconds, causing Time.deltaTime = 1/60 seconds.
        //float t = Time.deltaTime;
        HandleMovement();

        //use _jumpAction.started += JumpFunction;
        //NOT in Update();
        //Adding to transform.position.y;
    }

    private void OnDisable()
    {
        _playerActionsAsset.Disable();
    }

    private void HandleMovement()
    {
        //Check if player is pressed WASD.
        Vector2 inputMoveVector = _moveAction.ReadValue<Vector2>().normalized;

        //Magnitude in this case really only means if the player is pressing WASD. magnitude == 0 if WASD is NOT pressed.
        //Square roots are slow in computing, so grabbing the SQUARED magnitude is a slight optimization.
        if (inputMoveVector.sqrMagnitude > 0.01f)
        {
            Vector3 moveDirection = new Vector3(inputMoveVector.x, inputMoveVector.y);
            transform.position += moveDirection * Time.deltaTime * _playerSpeed;
        }

        /*
            ***Moving 1 unit (normalized vector value) every frame. @60FPS, 60u/second.
        transform.position += moveDirection;

            ***Multiplying by Time.deltaTime brings it "back within range" so instead of moving 60units/second, you move 1unit/second, effectively "normalizing" the rate of speed.
        transform.position += moveDirection * Time.deltaTime;

            ***Finally, by adding in our OWN variable (not deltaTime or inputValue), we can fully control how fast the player can move.
        */
    }
}
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