using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Weapon : MonoBehaviour
{
    [Header("Weapon Stats")]
    [SerializeField] private float shootIntervalInSeconds;
    [Header("Bullets")]
    public Bullet bullet;
    [SerializeField] private Transform bulletSpawnPoint;
    [Header("Bullet Pool")]
    private IObjectPool<Bullet> objectPool;
    private readonly bool collectionCheck = false;
    private readonly int defaultCapacity = 30;
    private readonly int maxSize = 100;
    private float timer;
    public Transform parentTransform;
    public Vector2 shootDirection = Vector2.up; // Default shooting direction is up

    private void Awake()
    {
        objectPool = new ObjectPool<Bullet>(CreateBullet, onGetFromPool, onReleaseToPool, OnDestroyPooledObject, collectionCheck, defaultCapacity, maxSize);
    }

    Bullet CreateBullet()
    {
        Bullet newBullet = Instantiate(bullet, bulletSpawnPoint.position, bulletSpawnPoint.rotation, parentTransform);
        newBullet.objectPool = objectPool;
        return newBullet;
    }

    void onGetFromPool(Bullet pooledObject)
    {
        pooledObject.gameObject.SetActive(true);
    }

    void onReleaseToPool(Bullet pooledObject)
    {
        pooledObject.gameObject.SetActive(false);
    }

    void OnDestroyPooledObject (Bullet pooledObject)
    {
        Destroy(pooledObject.gameObject);
    }

    void FixedUpdate()
    {
        if (Time.time > timer && objectPool != null && Player.Instance.Weapon != null)
        {
            Bullet bulletObject = objectPool.Get();

            if(bulletObject == null)
            {
                return;
            }
            
            bulletObject.transform.SetPositionAndRotation(bulletSpawnPoint.position, bulletSpawnPoint.rotation);

            bulletObject.GetComponent<Rigidbody2D>().velocity = shootDirection * bulletObject.bulletSpeed;
            
            bulletObject.Deactivate();  

            timer = Time.time + shootIntervalInSeconds; 
        }
    }
}
