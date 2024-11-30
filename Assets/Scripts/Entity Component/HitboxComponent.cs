using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class HitboxComponent : MonoBehaviour
{
    [SerializeField] private HealthComponent health;

    public void Damage(int damage)
    {
        if (CanTakeDamage())
        {
            health.SubtractHealth(damage);
        }
    }

    public void Damage(Bullet bullet)
    {
        if (CanTakeDamage())
        {
            health.SubtractHealth(bullet.damage);
        }
    }

    private bool CanTakeDamage()
    {
        var invincibilityComponent = GetComponent<InvincibilityComponent>();
        return invincibilityComponent == null || !invincibilityComponent.isInvincible;
    }
}
