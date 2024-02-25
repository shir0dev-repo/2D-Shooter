using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : Attack
{
    [SerializeField] private GameObject _projectilePrefab;
    [SerializeField] private PlayerInputHandler _playerInputHandler;
    [SerializeField] private AudioClip _attackSound;

    private void Awake()
    {
        if (_playerInputHandler == null)
            _playerInputHandler = GetComponent<PlayerInputHandler>();
    }

    private void Start()
    {

        _playerInputHandler.AttackAction.started += RegisterInput;
    }

    private void OnDisable()
    {
        _playerInputHandler.AttackAction.started -= RegisterInput;
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