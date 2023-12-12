using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
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
        _playerInputHandler.AttackAction.started += HandleAttack;
    }

    private void OnDisable()
    {
        _playerInputHandler.AttackAction.started -= HandleAttack;
    }

    public void HandleAttack(InputAction.CallbackContext context)
    {
        AudioManager.Instance.PlayAudio(_attackSound);
        Projectile projectile = Instantiate(_projectilePrefab, transform.position, Quaternion.identity).GetComponent<Projectile>();
        projectile.SetDirection(PlayerInputHandler.GetMousePosition());

        StartCoroutine(CameraController.Instance.CursorExpandCoroutine());
    }
}