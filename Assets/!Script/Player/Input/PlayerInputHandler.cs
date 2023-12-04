using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    private const string _MOVE_ACTION_NAME = "Move";
    private const string _ATTACK_ACTION_NAME = "Attack";
    private const string _JUMP_ACTION_NAME = "Jump";

    //GETtable by any script, but not SETtable.
    public static PlayerInputHandler Instance { get; private set; }
    public InputAction MoveAction { get { return _moveAction; } }
    public InputAction JumpAction { get { return _jumpAction; } }
    public InputAction AttackAction { get { return _attackAction; } }

    private InputAction _moveAction;
    private InputAction _jumpAction;
    private InputAction _attackAction;

    private PlayerInputActionsAsset _playerActionsAsset;

    private void Awake()
    {
        //Checks if a "copy" of PlayerInputHandler exists in game world, destroys THIS copy, if it does. Should ONLY be on player (ONE copy).
        if (Instance != null)
        {
            Debug.LogAssertion("ONLY ONE INSTANCE OF PLAYERINPUTHANDLER ALLOWED!!!");
            Destroy(gameObject);
        }
        //No copy found, make THIS instance of PlayerInputHandler the "copy."
        else
            Instance = this;

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
        return new Vector2(mousePos.x, mousePos.y);
    }
}