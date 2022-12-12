using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditorInternal.ReorderableList;

public class BulletHandler : MonoBehaviour
{
    [SerializeField] Bullet prefab;
    private List<Bullet> pool;
    [SerializeField] int poolSize = 10;
    [SerializeField] int nrOfActiveObjects = 0;

    public static BulletHandler instance;
    void Start()
    {
        pool = new List<Bullet>(poolSize);
        for (int i = 0; i < poolSize; i++)
        {
            pool.Add(createNewObject());
        }
        instance = this;
    }

    Bullet createNewObject()
    {
        Bullet gm = Instantiate(prefab);
        gm.transform.parent = transform;
        gm.gameObject.SetActive(false);
        return gm;
    }

    void IncreasePool()
    {
        pool.Capacity *= 2;
        for (int i = nrOfActiveObjects; i < pool.Capacity; i++)
        {
            pool.Add(createNewObject());
        }
    }

    public Bullet RetrieveInstance(Vector3 position, Quaternion rotation)
    {
        if (nrOfActiveObjects >= pool.Capacity)
            IncreasePool();

        var currentBullet = pool[nrOfActiveObjects];
        currentBullet.PoolIndex = nrOfActiveObjects;
        currentBullet.gameObject.SetActive(true);
        currentBullet.transform.position = position;
        currentBullet.transform.rotation = rotation;


        nrOfActiveObjects += 1;
        return currentBullet;
    }
    
    public void DestroyBullet(Bullet b)
    {
        b.gameObject.SetActive(false);
        nrOfActiveObjects-= 1;
        pool[b.PoolIndex] = pool[nrOfActiveObjects];
        pool[nrOfActiveObjects] = b;
    }
}
