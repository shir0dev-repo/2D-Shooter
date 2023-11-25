using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyMovement : MonoBehaviour, IDamageable
{
    [SerializeField] private float _enemyMoveSpeed = 8f;
    [SerializeField] private float _enemyMaxMoveSpeed = 10f;
    [SerializeField] private Rigidbody2D _rigidbody;



    private void Awake()
    {
        if (_rigidbody == null)
            _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        HandleEnemyMovement();
    }

    public void TakeDamage()
    {
        EnemySpawner.OnEnemyKilled?.Invoke();
        Destroy(gameObject);
    }

    private void HandleEnemyMovement()
    {
        Vector3 horizontalVelocity = _rigidbody.velocity;
        horizontalVelocity.y = 0;

        Vector3 playerPosition = GameManager.Instance.PlayerPosition;


        Vector3 direction = (playerPosition - transform.position).normalized;
        float facingDirection = direction.x > 0 ? 180f : 0f;
        transform.rotation = Quaternion.Euler(0, facingDirection, 0);

        direction.y = 0;

        if (horizontalVelocity.sqrMagnitude >= _enemyMaxMoveSpeed * _enemyMaxMoveSpeed)
        {
            direction *= _enemyMaxMoveSpeed;
            _rigidbody.velocity = direction;
        }
        else
        {
            _rigidbody.AddForce(direction * _enemyMoveSpeed * Time.fixedDeltaTime, ForceMode2D.Impulse);
        }
    }
}

/*

- direction of movement (towards the player)
- cap the speed if >= maxspeed variable
- 

*/