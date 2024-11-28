using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float rotateSpeed;
    [SerializeField] public Camera mainCamera;
    Vector2 newPosition;
    Vector2 velocity;
    GameObject player;
    private Vector2 screenBounds;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        ChangePosition();
        newPosition = new Vector2(-10, 10);
        screenBounds = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.transform.position.z));
        velocity = new Vector2(speed, speed);
    }

    void Update()
    {
        if (Vector2.Distance(transform.position, newPosition) < 0.5f)
        {
            ChangePosition();
        }

        if (player != null && player.GetComponent<Player>().Weapon)
        {
            gameObject.GetComponent<Renderer>().enabled = true;
            gameObject.GetComponent<Collider2D>().enabled = true;
        }
        else
        {
            gameObject.GetComponent<Renderer>().enabled = false;
            gameObject.GetComponent<Collider2D>().enabled = false;
        }

        transform.position += (Vector3)velocity * Time.deltaTime;
        transform.Rotate(Vector3.forward * rotateSpeed * Time.deltaTime);

        BounceWithinBounds();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            LevelManager levelManager = FindObjectOfType<LevelManager>();
            if (levelManager != null)
            {
                levelManager.LoadScene("Main");
            }
        }
    }

    void ChangePosition()
    {
        float cameraHeight = 2f * mainCamera.orthographicSize;
        float cameraWidth = cameraHeight * mainCamera.aspect;

        float minX = mainCamera.transform.position.x - cameraWidth / 2;
        float maxX = mainCamera.transform.position.x + cameraWidth / 2;
        float minY = mainCamera.transform.position.y - cameraHeight / 2;
        float maxY = mainCamera.transform.position.y + cameraHeight / 2;

        newPosition = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
    }

    void BounceWithinBounds()
    {
        Vector3 viewPos = transform.position;
        if (viewPos.x <= screenBounds.x * -1 || viewPos.x >= screenBounds.x)
        {
            velocity.x = -velocity.x;
        }
        if (viewPos.y <= screenBounds.y * -1 || viewPos.y >= screenBounds.y)
        {
            velocity.y = -velocity.y;
        }
    }
}