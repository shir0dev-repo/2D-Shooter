using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    //bullet script, bullet prefab, ref to attack action
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
        //Instantiate(), bullet script
        //write the line of code to SPAWN the BULLET PREFAB
        //Move the bullet towards the mouse
        GameObject bullet = Instantiate(_projectilePrefab, transform.position, Quaternion.identity);

        bullet.transform.up = PlayerInputHandler.GetMousePosition();
    }
}
