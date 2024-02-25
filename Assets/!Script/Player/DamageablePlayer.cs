using System.Collections;
using UnityEngine;

public class DamageablePlayer : Damageable
{
    [SerializeField] private AudioClip _deathSound;
    [SerializeField] private float _invulnTimer;
    public override void TakeDamage(int damageAmount)
    {
        base.TakeDamage(damageAmount);
        StartCoroutine(InvulnerabilityFramesCoroutine());
    }
    public void Heal(int healAmount)
    {
        _currentHealth = Mathf.Clamp(_currentHealth + healAmount, 0, _maxHealth);
    }
    public override void Die()
    {
        base.Die(); //check if health is ACTUALLY zero.

        MainManager.Instance.AudioManager.PlaySoundEffect(_deathSound);

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

    IEnumerator InvulnerabilityFramesCoroutine()
    {
        _isInvulnerable = true;
        yield return new WaitForSeconds(_invulnTimer);
        _isInvulnerable = false;
    }
}
