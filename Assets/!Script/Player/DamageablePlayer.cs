using UnityEngine;

public class DamageablePlayer : Damageable
{
    [SerializeField] private AudioClip _deathSound;
    public override void TakeDamage(int damageAmount)
    {
        base.TakeDamage(damageAmount);
    }

    public override void Die()
    {
        base.Die(); //check if health is ACTUALLY zero.

        Debug.Log("dead");
        AudioManager.Instance.PlayAudio(_deathSound);

        PlayerAnimationHandler animHandler = GetComponentInChildren<PlayerAnimationHandler>();
        if (animHandler != null)
        {
            animHandler.DeathAnimation(KillPlayer);
        }

    }

    private void KillPlayer()
    {
        GameManager.OnPlayerDeath?.Invoke();
        Destroy(gameObject);
    }
}
