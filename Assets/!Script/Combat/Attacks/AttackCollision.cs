using UnityEngine;

public class AttackCollision : Attack
{
    [SerializeField] private bool _destroyOffScreen;
    private Collider2D _collider;
    bool _isTrigger;

    bool _readyToDestroy = false;

    private void Start()
    {
        _collider = GetComponent<Collider2D>();

        _isTrigger = _collider.isTrigger;
    }

    protected override void Update()
    {
        base.Update();

        if (_attackReady && !_readyToDestroy)
            HandleAttack();

        if (_readyToDestroy)
        {
            float xCutoff = CameraController.GetOffScreenPosition().x;

            if (transform.position.x < xCutoff)
            {
                Destroy(gameObject);
                EnemySpawner.OnEnemyKilled?.Invoke();
            }
        }
    }

    public override void HandleAttack()
    {
        if (_destroyOffScreen)
            _readyToDestroy = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (_isTrigger) return;

        if (!collision.gameObject.TryGetComponent(out IDamageable damageable))
            return;

        if (((1 << collision.gameObject.layer) & _targetLayer) != 0)
        {
            damageable.TakeDamage(_damage);
            base.HandleAttack();
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (_isTrigger) return;

        if (!_attackReady) return;

        if (!collision.gameObject.TryGetComponent(out IDamageable damageable)) return;

        if (((1 << collision.gameObject.layer) & _targetLayer) != 0)
        {
            damageable.TakeDamage(_damage);
            base.HandleAttack();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!_isTrigger) return;

        if (!other.gameObject.TryGetComponent(out IDamageable damageable)) return;

        if (((1 << other.gameObject.layer) & _targetLayer) != 0)
        {
            damageable.TakeDamage(_damage);
            base.HandleAttack();
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (!_isTrigger) return;

        if (!_attackReady) return;

        if (!other.TryGetComponent(out IDamageable damageable)) return;

        if (((1 << other.gameObject.layer) & _targetLayer) != 0)
        {
            damageable.TakeDamage(_damage);
            base.HandleAttack();
        }
    }
}
