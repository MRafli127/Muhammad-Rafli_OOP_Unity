using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class AttackComponent : MonoBehaviour
{
    [SerializeField] private Bullet bullet;
    [SerializeField] private int damage;

    void OnTriggerEnter2D(Collider2D other)
    {
        // Skip interaction with objects of the same tag
        if (other.CompareTag(gameObject.tag)) return;

        var hitbox = other.GetComponent<HitboxComponent>();
        if (hitbox != null)
        {
            // Apply damage via bullet or raw damage
            if (bullet != null)
            {
                hitbox.Damage(bullet);
            }
            else
            {
                hitbox.Damage(damage);
            }

            // Trigger invincibility if the component exists
            var invincibilityComponent = other.GetComponent<InvincibilityComponent>();
            invincibilityComponent?.TriggerInvincibility();
        }
    }
}
