using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    private const string _MOVE_ACTION_NAME = "Move";
    private const string _ATTACK_ACTION_NAME = "Attack";
    private const string _JUMP_ACTION_NAME = "Jump";

    private PlayerInputActionsAsset _playerActionsAsset;

    private InputAction _moveAction;
    private InputAction _jumpAction;
    private InputAction _attackAction;

    //GETtable by PlayerMovement script, but not SETtable.
    public InputAction MoveAction { get { return _moveAction; } }
    public InputAction JumpAction { get { return _jumpAction; } }
    public InputAction AttackAction { get { return _attackAction; } }

    private void Awake()
    {
        //Create new "instance" or "copy" of ActionsAsset
        _playerActionsAsset = new PlayerInputActionsAsset();

        //Get reference to specific action from the NAME IN ACTION ASSET IN UNITY
        _jumpAction = _playerActionsAsset.FindAction(_JUMP_ACTION_NAME);
        _moveAction = _playerActionsAsset.FindAction(_MOVE_ACTION_NAME);
        _attackAction = _playerActionsAsset.FindAction(_ATTACK_ACTION_NAME);
    }

    private void OnEnable()
    {
        _playerActionsAsset.Enable();
    }

    private void OnDisable()
    {
        _playerActionsAsset.Disable();
    }

    public static Vector2 GetMousePosition()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return new Vector2(mousePos.x, mousePos.y).normalized;
    }
}

//Magnitude in this case really only means if the player is pressing WASD. magnitude == 0 if WASD is NOT pressed.
//Square roots are slow in computing, so grabbing the SQUARED magnitude is a slight optimization.

/*
        if (inputMoveVector.sqrMagnitude > 0.01f)
        {
            moveDirection = new Vector3(inputMoveVector.x, inputMoveVector.y);
            transform.position += moveDirection * Time.deltaTime * _playerMoveSpeed; //realistically, this is ALL you need to move something in unity. (in update method) teehee
        }

*/