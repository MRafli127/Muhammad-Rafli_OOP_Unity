using UnityEngine;

public class Player : MonoBehaviour {
    public static Player Instance { get; private set; }
    private PlayerMovement playerMovement;
    private Animator animator;

    private void Awake(){
        if (Instance == null){
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }else{
            Destroy(gameObject);
        }
    }

    private void Start(){
        playerMovement = GetComponent<PlayerMovement>();
        if (playerMovement == null) {
            Debug.LogError("PlayerMovement component not found on Player GameObject.");
        }

        animator = transform.Find("EngineEffect")?.GetComponent<Animator>();
        if (animator == null)
        {
        
        }
    }

    private void FixedUpdate() {
        playerMovement?.Move();
    }

    private void LateUpdate() {
        if (playerMovement != null && animator != null)
        {
            animator.SetBool("IsMoving", playerMovement.IsMoving());
        }
    }
}
