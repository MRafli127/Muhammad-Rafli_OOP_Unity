using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    // Senjata yang akan dihubungkan di Inspector Unity
    [SerializeField] Weapon weaponHolder;

    private Weapon weapon;

    // Inisialisasi weapon dari weaponHolder
    void Awake()
    {
        weapon = weaponHolder;
    }

    // Start dipanggil sebelum frame pertama update
    void Start()
    {
        // Menyembunyikan senjata jika weapon tidak null
        if (weapon != null)
        {
            TurnVisual(false);  // Menyembunyikan tampilan weapon di awal
        }
    }

    // Fungsi ini dipanggil saat ada collider lain yang masuk ke dalam trigger
    void OnTriggerEnter2D(Collider2D other)
{
    Debug.Log("Triggered by " + other.name);  // Untuk melihat objek apa yang trigger
    if (other.CompareTag("Player"))
    {
        Debug.Log("Player detected!");
        weapon.transform.SetParent(other.transform);
        weapon.transform.localPosition = Vector3.zero;
        TurnVisual(true);
        Debug.Log("Weapon picked up by player.");
    }
    else
    {
        Debug.Log("Collision detected, but not with Player.");
    }
}

    // Mengatur visibilitas komponen visual senjata
    void TurnVisual(bool on)
    {
        if (weapon != null)
        {
            weapon.gameObject.SetActive(on);
        }
    }

    // Method tambahan untuk memeriksa apakah weaponHolder memiliki senjata
    public bool GetWeapon()
    {
        return weaponHolder != null;
    }
}
