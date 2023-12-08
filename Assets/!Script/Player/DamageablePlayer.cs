public class DamageablePlayer : Damageable
{
    public override void TakeDamage(int damageAmount)
    {
        base.TakeDamage(damageAmount);
    }

    public override void Die()
    {
        base.Die(); //check if health is ACTUALLY zero.

        GameManager.Instance.OnPlayerDeath?.Invoke();

        PlayerAnimationHandler animHandler = GetComponentInChildren<PlayerAnimationHandler>();
        if (animHandler != null)
        {
            animHandler.DeathAnimation(KillPlayer);
        }

    }

    private void KillPlayer()
    {
        Destroy(gameObject);
    }
}
