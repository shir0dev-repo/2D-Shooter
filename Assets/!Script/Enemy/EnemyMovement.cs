using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyMovement : MonoBehaviour
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
        /*        Vector3 playerPosition = PlayerMovement.Instance.PlayerPosition;
                playerPosition.y = 0;

                Vector3 rigidbodyHorizontalVelocity = _rigidbody.velocity;
                float rigidbodyVerticalVelocity = _rigidbody.velocity.y;

                rigidbodyHorizontalVelocity.y = 0;

                //Checking magnitude >= a max speed
                if (rigidbodyHorizontalVelocity.sqrMagnitude >= _enemyMaxMoveSpeed * _enemyMaxMoveSpeed)
                {
                    //if the magnitude is bigger, you are setting velocity to max speed in the RIGHT direction
                    Vector3 direction = playerPosition - transform.position;
                    Vector3 moveDirection = transform.position + (_enemyMoveSpeed * Time.fixedDeltaTime * direction);

                    _rigidbody.velocity = moveDirection.normalized;
                    Debug.Log("Enemy Capped Speed! " + _rigidbody.velocity);
                }
                else
                {
                    Vector3 direction = playerPosition - transform.position;
                    Vector3 moveDirection = transform.position + _enemyMoveSpeed * Time.fixedDeltaTime * direction;
                    _rigidbody.AddForce(moveDirection, ForceMode2D.Impulse);
                }*/
    }

    private void HandleEnemyMovement()
    {
        Vector3 horizontalVelocity = _rigidbody.velocity;
        horizontalVelocity.y = 0;

        Vector3 playerPosition = PlayerMovement.Instance.transform.position;
        Vector3 direction = (playerPosition - transform.position).normalized;
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(typeof(Projectile), out _))
        {
            EnemySpawner.OnEnemyKilled?.Invoke();
            Destroy(gameObject);
        }
    }
}

/*

- direction of movement (towards the player)
- cap the speed if >= maxspeed variable
- 

*/