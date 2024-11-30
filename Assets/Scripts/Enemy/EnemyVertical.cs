using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVertical : Enemy
{
    protected override void Initialize()
    {
        float spawnX = Random.Range(-screenBounds.x + spriteHalfWidth, screenBounds.x - spriteHalfWidth);
        float spawnY = screenBounds.y + spriteHalfHeight;

        transform.position = new Vector2(spawnX, spawnY);
        direction = Vector2.down;
    }

    protected override void Move()
    {
        transform.Translate(direction * speed * Time.deltaTime);

        if (transform.position.y < -screenBounds.y - spriteHalfHeight)
        {
            Respawn();
        }
    }

    protected override void Respawn()
    {
        float spawnX = Random.Range(-screenBounds.x + spriteHalfWidth, screenBounds.x - spriteHalfWidth);
        float spawnY = screenBounds.y + spriteHalfHeight;

        transform.position = new Vector2(spawnX, spawnY);
        direction = Vector2.down;
    }
}