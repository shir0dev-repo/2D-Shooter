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
        Debug.Log($"MouseX: {mousePos.x} MouseY: {mousePos.y}");
        return new Vector2(mousePos.x, mousePos.y);
    }
}