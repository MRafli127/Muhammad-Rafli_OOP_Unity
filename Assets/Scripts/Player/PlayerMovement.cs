using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Vector2 maxSpeed;
    [SerializeField] private Vector2 timeToFullSpeed;
    [SerializeField] private Vector2 timeToStop;
    [SerializeField] private Vector2 stopClamp;

    private Vector2 moveDirection;
    private Vector2 moveVelocity;
    private Vector2 moveFriction;
    private Vector2 stopFriction;
    private Rigidbody2D rb;

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null){
            Debug.LogError("Rigidbody2D component not found on PlayerMovement GameObject.");
        }
        moveVelocity = 2 * maxSpeed / timeToFullSpeed;
        moveFriction = -2 * maxSpeed / (timeToFullSpeed * timeToFullSpeed);
        stopFriction = -2 * maxSpeed / (timeToStop * timeToStop);
    }

    public void Move() {
        moveDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        Vector2 velocity = moveDirection * moveVelocity * Time.fixedDeltaTime;

        rb.velocity = new Vector2(
            Mathf.Clamp(rb.velocity.x + velocity.x, -maxSpeed.x, maxSpeed.x),
            Mathf.Clamp(rb.velocity.y + velocity.y, -maxSpeed.y, maxSpeed.y)
        );

        rb.velocity += GetFriction() * Time.fixedDeltaTime;

        if (Mathf.Abs(rb.velocity.x) < stopClamp.x) rb.velocity = new Vector2(0, rb.velocity.y);
        if (Mathf.Abs(rb.velocity.y) < stopClamp.y) rb.velocity = new Vector2(rb.velocity.x, 0);
    }

    private Vector2 GetFriction() {
        return new Vector2(
            moveDirection.x != 0 ? moveFriction.x : stopFriction.x,
            moveDirection.y != 0 ? moveFriction.y : stopFriction.y
        );
    }

    public bool IsMoving() {
        return rb.velocity.magnitude > 0.1f;
    }
}
