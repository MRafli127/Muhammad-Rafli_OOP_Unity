using System.Collections;
using UnityEngine;

public class InvincibilityComponent : MonoBehaviour
{
    [SerializeField] private int blinkCount = 3;
    [SerializeField] private float blinkInterval = 0.1f;
    [SerializeField] private Material blinkMaterial;

    private SpriteRenderer spriteRenderer;
    private Material originalMaterial;
    public bool isInvincible = false;

    void Start()
    {
        // Get the sprite renderer from the correct object
        spriteRenderer = (gameObject.CompareTag("Player") && GameObject.Find("Ship") != null)
            ? GameObject.Find("Ship").GetComponent<SpriteRenderer>()
            : GetComponent<SpriteRenderer>();

        if (spriteRenderer != null)
        {
            originalMaterial = spriteRenderer.material;
        }
    }

    public void TriggerInvincibility()
    {
        if (!isInvincible && spriteRenderer != null)
        {
            StartCoroutine(BlinkEffect());
        }
    }

    private IEnumerator BlinkEffect()
    {
        isInvincible = true;
        for (int i = 0; i < blinkCount; i++)
        {
            spriteRenderer.material = blinkMaterial;
            yield return new WaitForSeconds(blinkInterval);
            spriteRenderer.material = originalMaterial;
            yield return new WaitForSeconds(blinkInterval);
        }
        isInvincible = false;
    }
}
