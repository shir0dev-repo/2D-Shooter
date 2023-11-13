using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private GameObject _projectilePrefab;
    [SerializeField] private PlayerInputHandler _playerInputHandler;

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

    private void HandleAttack(InputAction.CallbackContext context)
    {
        Projectile projectile = Instantiate(_projectilePrefab, transform.position, Quaternion.identity).GetComponent<Projectile>();

        projectile.SetDirection(PlayerInputHandler.GetMousePosition());
    }
}