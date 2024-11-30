using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

public class EnemyTargeting : Enemy
{
    private Transform player;

    protected override void Initialize()
    {
        player = Player.Instance?.transform;

        float spawnX = Random.Range(-screenBounds.x + spriteHalfWidth, screenBounds.x - spriteHalfWidth);
        float spawnY = screenBounds.y + spriteHalfHeight;

        transform.position = new Vector2(spawnX, spawnY);
        direction = Vector2.down;
    }

    protected override void Move()
    {
        if (player != null)
        {
            Vector2 playerDirection = (player.position - transform.position).normalized;
            direction = Vector2.Lerp(direction, playerDirection, Time.deltaTime).normalized;
        }

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
