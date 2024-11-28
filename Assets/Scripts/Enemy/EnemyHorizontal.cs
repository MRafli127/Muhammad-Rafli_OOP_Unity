using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHorizontal : Enemy 
{
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
}
