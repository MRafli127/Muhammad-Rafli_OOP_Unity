using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoss : Enemy 
{
    [SerializeField] Weapon weapon;
    public Weapon Weapon { get; set; }
    protected override void Initialize()
    {
        float spawnX;
        if (Random.Range(0, 2) == 0)
        {
            spawnX = -screenBounds.x - spriteHalfWidth;
        }
        else
        {
            spawnX = screenBounds.x + spriteHalfWidth;
        }
        float spawnY = Random.Range(-screenBounds.y + spriteHalfHeight, screenBounds.y - spriteHalfHeight);
        transform.position = new Vector2(spawnX, spawnY);
        if (spawnX < 0)
        {
            direction = Vector2.right;
        }
        else
        {
            direction = Vector2.left;
        }
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
        float spawnX;
        if (direction == Vector2.right)
        {
            spawnX = -screenBounds.x - spriteHalfWidth;
        }
        else
        {
            spawnX = screenBounds.x + spriteHalfWidth;
        }
        float spawnY = Random.Range((-screenBounds.y + spriteHalfHeight)/2, screenBounds.y - spriteHalfHeight);
        transform.position = new Vector2(spawnX, spawnY);
        if (spawnX < 0)
        {
            direction = Vector2.right;
        }
        else
        {
            direction = Vector2.left;
        }
        speed = -speed; // Reverse the velocity
    }

    protected override void Start()
    {
        base.Start();
        // Ensure this code runs only once for the instantiated object
        if (weapon != null)
        {
            // Deactivate all other weapons except the player's weapon
            Weapon[] allWeapons = FindObjectsOfType<Weapon>();
            foreach (Weapon w in allWeapons)
            {
                if (w != this.weapon && w.transform.parent != Player.Instance.transform)
                {
                    w.gameObject.SetActive(false);
                }
            }

            if (Weapon != null)
            {
                Weapon.transform.SetParent(null);
                Weapon.gameObject.SetActive(false);
            }
            Weapon = Instantiate(weapon, transform.position, Quaternion.identity);
            Weapon.transform.SetParent(transform);
            Weapon.transform.localPosition = new Vector3(0, 0, 0);
            Weapon.shootDirection = Vector2.down; // Set shooting direction to down
            Weapon.gameObject.SetActive(true);
            TurnVisual(false);
        }
    }

    void TurnVisual(bool on)
    {
        foreach (var component in weapon.GetComponents<Component>())
        {
            TurnVisual(on, weapon.GetComponent<Weapon>());
        }   
    }

    void TurnVisual(bool on, Weapon weapon)
    {   
        weapon.GetComponent<SpriteRenderer>().enabled = on;
        weapon.GetComponent<Animator>().enabled = on;
    }
}
