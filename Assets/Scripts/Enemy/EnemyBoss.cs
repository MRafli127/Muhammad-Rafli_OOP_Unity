using System.Collections;
using UnityEngine;

public class EnemyBoss : Enemy
{
    [SerializeField] private Weapon weaponPrefab;
    public Weapon WeaponInstance { get; private set; }

    protected override void Initialize()
    {
        float spawnX = (Random.Range(0, 2) == 0) ? -screenBounds.x - spriteHalfWidth : screenBounds.x + spriteHalfWidth;
        float spawnY = Random.Range(-screenBounds.y + spriteHalfHeight, screenBounds.y - spriteHalfHeight);

        transform.position = new Vector2(spawnX, spawnY);
        direction = (spawnX < 0) ? Vector2.right : Vector2.left;
    }

    protected override void Move()
    {
        transform.Translate(direction * speed * Time.deltaTime);

        if (transform.position.x > screenBounds.x + spriteHalfWidth || transform.position.x < -screenBounds.x - spriteHalfWidth)
        {
            Respawn();
        }
    }

    protected override void Respawn()
    {
        float spawnX = (direction == Vector2.right) ? -screenBounds.x - spriteHalfWidth : screenBounds.x + spriteHalfWidth;
        float spawnY = Random.Range((-screenBounds.y + spriteHalfHeight) / 2, screenBounds.y - spriteHalfHeight);

        transform.position = new Vector2(spawnX, spawnY);
        direction = (spawnX < 0) ? Vector2.right : Vector2.left;
        speed = -speed; // Reverse the velocity
    }

    protected override void Start()
    {
        base.Start();
        if (weaponPrefab != null)
        {
            DeactivateOtherWeapons();

            WeaponInstance = Instantiate(weaponPrefab, transform.position, Quaternion.identity);
            WeaponInstance.transform.SetParent(transform);
            WeaponInstance.transform.localPosition = Vector3.zero;
            WeaponInstance.shootDirection = Vector2.down;
            WeaponInstance.gameObject.SetActive(true);

            SetWeaponVisuals(false);
        }
    }

    private void DeactivateOtherWeapons()
    {
        Weapon[] allWeapons = FindObjectsOfType<Weapon>();
        foreach (Weapon weapon in allWeapons)
        {
            if (weapon != weaponPrefab && weapon.transform.parent != Player.Instance.transform)
            {
                weapon.gameObject.SetActive(false);
            }
        }
    }

    private void SetWeaponVisuals(bool isActive)
    {
        foreach (var component in weaponPrefab.GetComponents<Component>())
        {
            SpriteRenderer spriteRenderer = weaponPrefab.GetComponent<SpriteRenderer>();
            Animator animator = weaponPrefab.GetComponent<Animator>();

            if (spriteRenderer) spriteRenderer.enabled = isActive;
            if (animator) animator.enabled = isActive;
        }
    }
}
