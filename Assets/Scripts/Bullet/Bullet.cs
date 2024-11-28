using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Bullet : MonoBehaviour
{
    [Header("Bullet Stats")]
    public float bulletSpeed = 20;
    public int damage = 10;
    private Rigidbody2D rb;

    public IObjectPool<Bullet> objectPool;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    IEnumerator DeactivateBullet(float delay)
    {
        yield return new WaitForSeconds(delay);

        rb.velocity = Vector3.zero;
        rb.angularVelocity = 0f;
        objectPool.Release(this);
    }

    public void Deactivate ()
    {
        StartCoroutine(DeactivateBullet(2f));
    }
}
