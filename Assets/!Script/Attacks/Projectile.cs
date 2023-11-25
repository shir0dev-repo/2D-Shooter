using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Projectile : MonoBehaviour
{
    [SerializeField] private float _maxLifetimeInSeconds = 0.5f;
    [SerializeField] private float _projectileSpeed = 1f;
    [SerializeField] private LayerMask _targetLayer;

    public LayerMask TargetLayer { get { return _targetLayer; } }

    private Vector3 _direction = Vector3.zero;
    public void SetDirection(Vector3 dir)
    {
        _direction = (dir - transform.position).normalized;
    }

    private float _despawnTimer = 0f;

    private void Update()
    {
        _despawnTimer += Time.deltaTime;

        if (_despawnTimer >= _maxLifetimeInSeconds)
            Destroy(gameObject);
        else
            transform.position += _projectileSpeed * Time.deltaTime * _direction;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (!other.TryGetComponent(out IDamageable damageable)) //If collider does NOT have IDamageable interface, return.
            return;

        if (((1 << other.gameObject.layer) & TargetLayer) != 0) //If collider layer matches target layer, take damage.
        {
            damageable.TakeDamage();
            Destroy(gameObject);
        }
    }
}

/*
 
1 << LAYER: Ignores all other layers, isolating specific layer.
1 >> LAYER: Ignores PROVIDED layer, returns all others.

<<: SHIFT operator (left): Shifts left side by right side amount of bits
    (1)  00000001 <<
    (3)  00000011 ==
    (6) 00000110

>> SHIFT (right): Shifts left side by right side amount of bits.
    (1) 00000001 >>
    (3) 00000110 ==
        00000001

&: Bitwise AND operator: 
    10101011 &
    00110111 ==
    00100011
|: Bitwise OR operator: 
    10101011 |
    00110111 ==
    10111111
*/