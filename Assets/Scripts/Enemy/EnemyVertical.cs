using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVertical : Enemy
{
    protected override void Initialize()
    {
        float spawnY;
        if (Random.Range(0, 2) == 0)
        {
            spawnY = -screenBounds.y - spriteHalfHeight;
        }
        else
        {
            spawnY = screenBounds.y + spriteHalfHeight;
        }
        float spawnX = Random.Range((-screenBounds.x + spriteHalfWidth)/2, screenBounds.x - spriteHalfWidth);
        transform.position = new Vector2(spawnX, spawnY);
        if (spawnY < 0)
        {
            direction = Vector2.up;
        }
        else
        {
            direction = Vector2.down;
        }
    }

    protected override void Move()
    {
        transform.Translate(direction * speed * Time.deltaTime);

        if (transform.position.y > screenBounds.y + spriteHalfHeight || transform.position.y < -screenBounds.y - spriteHalfHeight)
        {
            Respawn();
        }
    }

    protected override void Respawn()
    {
        float spawnY;
        if (direction == Vector2.up)
        {
            spawnY = -screenBounds.y - spriteHalfHeight;
        }
        else
        {
            spawnY = screenBounds.y + spriteHalfHeight;
        }
        float spawnX = Random.Range(-screenBounds.x + spriteHalfWidth, screenBounds.x - spriteHalfWidth);
        transform.position = new Vector2(spawnX, spawnY);
        if (spawnY < 0)
        {
            direction = Vector2.up;
        }
        else
        {
            direction = Vector2.down;
        }
        speed = -speed; // Reverse the velocity
    }
}
