using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : Singleton<PlayerInputHandler>
{
    private const string _MOVE_ACTION_NAME = "Move";
    private const string _ATTACK_ACTION_NAME = "Attack";
    private const string _JUMP_ACTION_NAME = "Jump";
    private const string _PAUSE_ACTION_NAME = "Pause";

    public InputAction MoveAction { get { return _moveAction; } }
    public InputAction JumpAction { get { return _jumpAction; } }
    public InputAction AttackAction { get { return _attackAction; } }
    public InputAction PauseAction { get { return _pauseAction; } }

    private InputAction _moveAction;
    private InputAction _jumpAction;
    private InputAction _attackAction;
    private InputAction _pauseAction;

    private PlayerInputActionsAsset _playerActionsAsset;

    private static bool _isPaused = false;

    protected override void Awake()
    {
        //Create new "instance" or "copy" of ActionsAsset
        _playerActionsAsset = new PlayerInputActionsAsset();

        //Get reference to specific action from the NAME IN ACTION ASSET IN UNITY
        _jumpAction = _playerActionsAsset.FindAction(_JUMP_ACTION_NAME);
        _moveAction = _playerActionsAsset.FindAction(_MOVE_ACTION_NAME);
        _attackAction = _playerActionsAsset.FindAction(_ATTACK_ACTION_NAME);
        _pauseAction = _playerActionsAsset.FindAction(_PAUSE_ACTION_NAME);
    }


    private void EnableInput() => _playerActionsAsset.Enable();
    private void DisableInput() => _playerActionsAsset.Disable();

    private void OnEnable()
    {
        GameManager.OnPlayerDeath += DisableInput;
        _pauseAction.started += PauseGame;
        EnableInput();
    }

    private void OnDisable()
    {
        GameManager.OnPlayerDeath -= DisableInput;
        _pauseAction.started -= PauseGame;
        DisableInput();
    }

    public static Vector2 GetMousePosition()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return new Vector2(mousePos.x, mousePos.y);
    }

    void PauseGame(InputAction.CallbackContext ctx)
    {
        _isPaused = !_isPaused;
        if (_isPaused)
        {
            Cursor.visible = true;
            Time.timeScale = 0;
        }
        else
        {
            Cursor.visible = false;
            Time.timeScale = 1;
        }

        MainManager.Instance.PauseGame();
    }
}