using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHorizontal : Enemy
{
    private float speed = 5f;
    private Vector2 direction;

    private void Start()
    {
        direction = transform.position.x < 0 ? Vector2.right : Vector2.left;
    }

    private void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);

        if (Mathf.Abs(transform.position.x) > 10)
        {
            direction = -direction;
        }
    }
}

