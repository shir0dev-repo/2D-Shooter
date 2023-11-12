using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(Collider2D))]
public class Projectile : MonoBehaviour
{
    [SerializeField] private float _maxLifetimeInSeconds = 0.5f;
    [SerializeField] private float _projectileSpeed = 1f;

    private Vector3 _direction = Vector3.zero;
    public void SetDirection(Vector3 dir)
    {
        _direction = (dir - transform.position).normalized;
    }

    private float _despawnTimer = 0f;

    private void Update()
    {
        //every frame, despawn timer increases by deltaTime. Eventually, this will exceed _maxLifetime.
        _despawnTimer += Time.deltaTime;

        if (_despawnTimer >= _maxLifetimeInSeconds)
            Destroy(gameObject);
        else
            transform.position += _projectileSpeed * Time.deltaTime * _direction;
    }

    //A collider can move around other colliders, a trigger can't
    //triggers are used for when you need to check if one collider is on top of another, but dont want it to move the original gameobject.
    private void OnTriggerEnter2D(Collider2D other)
    {
        //bullet spawns on top of player, will destroy instantly if no check. 
        //projectile check for if two projectiles are overlapping
        if (!other.gameObject.CompareTag("Player") && !other.gameObject.CompareTag("Projectile"))
            Destroy(gameObject);
    }
}