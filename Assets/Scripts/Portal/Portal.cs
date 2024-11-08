using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float rotateSpeed;
    WeaponPickup weaponPickup;
    Vector2 newPosition;

    void Start()
    {
        weaponPickup = FindObjectOfType<WeaponPickup>();
        ChangePosition();
    }
    void Update()
    {
        if(Vector2.Distance(transform.position, newPosition) < 0.5f){
            ChangePosition();
        }

        transform.position = Vector2.MoveTowards(transform.position, newPosition, speed * Time.deltaTime);

        if(weaponPickup != null && !weaponPickup.GetWeapon()){
            gameObject.SetActive(false);       
            GetComponent<Collider2D>().enabled = false; 
        }
        else{
            gameObject.SetActive(true);        
            GetComponent<Collider2D>().enabled = true;  
        }
    }

    void OnTriggerEnter2D(Collider2D Other)
    {
        if(Other.CompareTag("Player")){
            GameManager.LevelManager.LoadScene("Main");
        }
    }

    void ChangePosition()
    {
        newPosition = new Vector2(Random.Range(-7.4f, 7.4f), Random.Range(-3.6f, 3.6f));
    }
}