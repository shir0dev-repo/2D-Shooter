using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyMovement : Movement
{
    [SerializeField] private LayerMask _targetLayer;
    [SerializeField] private float _initialMovementDelay;

    public override bool ToggleMovement(bool canMove)
    {
        _rigidbody.velocity = Vector2.zero;

        return base.ToggleMovement(canMove);
    }

    protected override void FixedUpdate()
    {
        if (_initialMovementDelay > 0)
        {
            _initialMovementDelay -= Time.fixedDeltaTime;
            return;
        }

        base.FixedUpdate();
    }

    protected override void HandleMovement()
    {
        Vector3 horizontalVelocity = _rigidbody.velocity;
        horizontalVelocity.y = 0;

        Vector3 playerPosition = MainManager.Instance.GameManager.PlayerPosition;

        Vector3 direction = (playerPosition - transform.position).normalized;
        float facingDirection = direction.x > 0 ? 180f : 0f;
        transform.rotation = Quaternion.Euler(0, facingDirection, 0);

        direction.y = 0;

        if (horizontalVelocity.sqrMagnitude >= _maxSpeed * _maxSpeed)
        {
            direction *= _maxSpeed;
            _rigidbody.velocity = direction;
        }
        else
        {
            _rigidbody.AddForce(_moveSpeed * Time.fixedDeltaTime * direction, ForceMode2D.Impulse);
        }
    }
}