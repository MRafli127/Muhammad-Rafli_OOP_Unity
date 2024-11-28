using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    [SerializeField] Weapon weaponHolder;
    Weapon weapon = null;

    void Awake()
    {
        weapon = Instantiate(weaponHolder);
    }

    void Start()
    {
        if (weapon != null)
        {
            TurnVisual(false);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                // Deactivate all other weapons except the player's weapon
                Weapon[] allWeapons = FindObjectsOfType<Weapon>();
                foreach (Weapon w in allWeapons)
                {
                    if (w != weapon && w.transform.parent != player.transform)
                    {
                        w.gameObject.SetActive(false);
                    }
                }

                if (player.Weapon != null)
                {
                    player.Weapon.transform.SetParent(null);
                    player.Weapon.gameObject.SetActive(false);
                }
                player.Weapon = weapon;
                weapon.transform.SetParent(player.transform);
                weapon.transform.localPosition = new Vector3(0, 0, 0);
                weapon.gameObject.SetActive(true);
                TurnVisual(true);
            }
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
