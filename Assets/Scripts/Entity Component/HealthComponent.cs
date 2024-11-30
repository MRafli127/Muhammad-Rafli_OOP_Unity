using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public int GetHealth()
    {
        return currentHealth;
    }

    public void SubtractHealth(int amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
