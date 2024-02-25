using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : Attack
{
    [SerializeField] private GameObject _projectilePrefab;
    [SerializeField] private GameObject _missilePrefab;
    [SerializeField] private PlayerInputHandler _playerInputHandler;
    [SerializeField] private AudioClip _attackSound;
    [SerializeField] private GameObject _missileSprite;
    [SerializeField] private AudioClip _meyow;
    private bool _hasMissile = false;

    public bool HasMissile => _hasMissile;

    private void Awake()
    {
        if (_playerInputHandler == null)
            _playerInputHandler = GetComponent<PlayerInputHandler>();

        _missileSprite.SetActive(false);
    }

    private void Start()
    {

        _playerInputHandler.AttackAction.started += RegisterInput;
        _playerInputHandler.MissileAction.started += SpawnMissile;
    }

    public void AddMissile()
    {
        _hasMissile = true;
        _missileSprite.SetActive(true);
    }

    private void SpawnMissile(InputAction.CallbackContext context)
    {
        if (!_hasMissile) return;

        MainManager.Instance.AudioManager.PlaySoundEffect(_meyow);

        Instantiate(_missilePrefab, transform.position, Quaternion.identity);

        _hasMissile = false;
        _missileSprite.SetActive(false);
    }

    private void OnDisable()
    {
        _playerInputHandler.AttackAction.started -= RegisterInput;
        _playerInputHandler.MissileAction.started -= SpawnMissile;
    }

    private void RegisterInput(InputAction.CallbackContext context)
    {
        if (!_attackReady) return;
        else
            HandleAttack();
    }
    public override void HandleAttack()
    {
        base.HandleAttack();

        MainManager.Instance.AudioManager.PlaySoundEffect(_attackSound);
        Projectile projectile = Instantiate(_projectilePrefab, transform.position, Quaternion.identity).GetComponent<Projectile>();
        projectile.SetDirection(PlayerInputHandler.GetMousePosition());

        StartCoroutine(MainManager.Instance.CameraController.CursorExpandCoroutine());
    }
}