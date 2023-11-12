using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.EventSystems;

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
        Vector3 rigidbodyHorizontalVelocity = _rigidbody.velocity;

        float rigidbodyVerticalVelocity = _rigidbody.velocity.y;

        rigidbodyHorizontalVelocity.y = 0;

        if (rigidbodyHorizontalVelocity.sqrMagnitude >= _enemyMaxMoveSpeed * _enemyMaxMoveSpeed)
        {
            _rigidbody.velocity = new Vector3(_enemyMaxMoveSpeed, rigidbodyVerticalVelocity);
            Debug.Log("Enemy Capped Speed! " + _rigidbody.velocity);
        }
        else
        {
            Vector3 moveDirection = _enemyMoveSpeed * Time.fixedDeltaTime * new Vector3(-1f, 0, 0).normalized;
            _rigidbody.AddForce(moveDirection, ForceMode2D.Impulse);

        }
    }
}
