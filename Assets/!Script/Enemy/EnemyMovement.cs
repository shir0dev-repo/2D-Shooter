using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyMovement : Movement
{
    [SerializeField] private LayerMask _targetLayer;
    public LayerMask TargetLayer { get { return _targetLayer; } }

    public override bool ToggleMovement(bool canMove)
    {
        _rigidbody.velocity = Vector2.zero;

        return base.ToggleMovement(canMove);
    }

    protected override void HandleMovement()
    {
        Vector3 horizontalVelocity = _rigidbody.velocity;
        horizontalVelocity.y = 0;

        Vector3 playerPosition = GameManager.Instance.PlayerPosition;

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
            _rigidbody.AddForce(direction * _moveSpeed * Time.fixedDeltaTime, ForceMode2D.Impulse);
        }
    }

    //FUTURE: Place this into AttackChase Script
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.TryGetComponent(out IDamageable damageable))
            return;

        if (((1 << collision.gameObject.layer) & TargetLayer) != 0)
        {
            damageable.TakeDamage(1);
        }
    }
}