using Unity.VisualScripting;
using UnityEngine;

public class HealthPickup : Pickup
{
    [SerializeField, Min(0)] private int _healAmount;
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out DamageablePlayer playerHP))
        {
            playerHP.Heal(_healAmount);

            Destroy(gameObject);
        }

    }
}
