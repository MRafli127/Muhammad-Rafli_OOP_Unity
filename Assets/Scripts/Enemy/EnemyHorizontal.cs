using UnityEngine;

public class EnemyHorizontal : Enemy
{
    protected override void Initialize()
    {
        float spawnX = (Random.Range(0, 2) == 0) ? -screenBounds.x - spriteHalfWidth : screenBounds.x + spriteHalfWidth;
        float spawnY = Random.Range(-screenBounds.y + spriteHalfHeight, screenBounds.y - spriteHalfHeight);

        transform.position = new Vector2(spawnX, spawnY);
        direction = (spawnX < 0) ? Vector2.right : Vector2.left;
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
        float spawnX = (direction == Vector2.right) ? -screenBounds.x - spriteHalfWidth : screenBounds.x + spriteHalfWidth;
        float spawnY = Random.Range((-screenBounds.y + spriteHalfHeight) / 2, screenBounds.y - spriteHalfHeight);

        transform.position = new Vector2(spawnX, spawnY);
        direction = (spawnX < 0) ? Vector2.right : Vector2.left;
        speed = -speed; // Reverse the velocity
    }
}
