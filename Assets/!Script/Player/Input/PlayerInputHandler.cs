using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : Singleton<PlayerInputHandler>
{
    private const string _MOVE_ACTION_NAME = "Move";
    private const string _ATTACK_ACTION_NAME = "Attack";
    private const string _JUMP_ACTION_NAME = "Jump";
    private const string _PAUSE_ACTION_NAME = "Pause";
    private const string _MISSILE_ACTION_NAME = "Missile";

    public InputAction MoveAction { get { return _moveAction; } }
    public InputAction JumpAction { get { return _jumpAction; } }
    public InputAction AttackAction { get { return _attackAction; } }
    public InputAction PauseAction { get { return _pauseAction; } }
    public InputAction MissileAction { get { return _missileAction; } }

    private InputAction _moveAction;
    private InputAction _jumpAction;
    private InputAction _attackAction;
    private InputAction _pauseAction;
    private InputAction _missileAction;

    private PlayerInputActionsAsset _playerActionsAsset;

    protected override void Awake()
    {
        //Create new "instance" or "copy" of ActionsAsset
        _playerActionsAsset = new PlayerInputActionsAsset();

        //Get reference to specific action from the NAME IN ACTION ASSET IN UNITY
        _jumpAction = _playerActionsAsset.FindAction(_JUMP_ACTION_NAME);
        _moveAction = _playerActionsAsset.FindAction(_MOVE_ACTION_NAME);
        _attackAction = _playerActionsAsset.FindAction(_ATTACK_ACTION_NAME);
        _pauseAction = _playerActionsAsset.FindAction(_PAUSE_ACTION_NAME);
        _missileAction = _playerActionsAsset.FindAction(_MISSILE_ACTION_NAME);
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
        MainManager.Instance.PauseGame();
        if (Time.timeScale < 1f)
        {
            _attackAction.Disable();
            _missileAction.Disable();
        }
        else
        {
            _missileAction.Enable();
            _attackAction.Enable();
        }
    }
}