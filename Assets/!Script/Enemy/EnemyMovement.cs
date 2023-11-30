using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyMovement : Movement, IDamageable
{
    [SerializeField] private LayerMask _targetLayer;
    public LayerMask TargetLayer { get { return _targetLayer; } }
    public void ToggleMovement(bool toggle)
    {
        _canMove = toggle;
    }


    protected override void Awake()
    {
        if (_rigidbody == null)
            _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void TakeDamage()
    {
        EnemySpawner.OnEnemyKilled?.Invoke();
        Destroy(gameObject);
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
    //private void OnTriggerEnter2D(Collider2D other)
    //{
    //    Debug.Log("Trigger Entered!");

    //    if (!other.TryGetComponent(out IDamageable damageable)) //If collider does NOT have IDamageable interface, return.
    //    {
    //        Debug.Log("No IDamagableComponentFound!");
    //        return;
    //    }

    //    if (((1 << other.gameObject.layer) & TargetLayer) != 0) //If collider layer matches target layer, take damage.
    //    {
    //        Debug.Log("Target layer matched!");
    //        damageable.TakeDamage();
    //        Destroy(gameObject);
    //    }
    //}
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.TryGetComponent(out IDamageable damageable))
            return;

        if (((1 << collision.gameObject.layer) & TargetLayer) != 0)
        {
            damageable.TakeDamage();  
            Destroy(gameObject);
        }
    }
}

/*

- direction of movement (towards the player)
- cap the speed if >= maxspeed variable
- 

*/