using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class Projectile : MonoBehaviour
{
    //rigidbody, collider, Destroy(), lifetime (float), speed (float)
    [SerializeField] Rigidbody2D _rigidbody;

    [SerializeField] private float _maxLifetimeInSeconds = 0.5f;
    [SerializeField] private float _velocity = 1f;

    private float _despawnTimer = 0f;

    private void Update()
    {
        
        //every frame, despawn timer increases by deltaTime. Eventually, this will exceed _maxLifetime.
        _despawnTimer += Time.deltaTime;

        if (_despawnTimer >= _maxLifetimeInSeconds)
            Destroy(gameObject);
    }

    private void FixedUpdate()
    {
        _rigidbody.AddForce(_velocity * Time.fixedDeltaTime * transform.up);
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