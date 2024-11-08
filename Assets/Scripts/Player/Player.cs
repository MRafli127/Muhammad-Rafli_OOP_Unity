using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;
    private PlayerMovement playerMovement;
    private Animator animator;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            if (transform.parent != null)
            {
                transform.SetParent(null);
            }
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != null)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();

        GameObject engineEffect = GameObject.Find("EngineEffect");
        if (engineEffect != null)
        {
            animator = engineEffect.GetComponent<Animator>();
        }
    }

    void FixedUpdate()
    {
        playerMovement?.Move();
    }

    void LateUpdate()
    {
        if (animator != null && playerMovement != null)
        {
            animator.SetBool("IsMoving", playerMovement.IsMoving());
        }
    }
}
