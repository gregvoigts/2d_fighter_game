using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditorInternal.ReorderableList;
using Mirror;

public class BulletHandler : NetworkBehaviour
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
                createNewObject();
                
            }
        
        instance = this;
    }

    void createNewObject()
    {
        Bullet gm = Instantiate(prefab);
        NetworkServer.Spawn(gm.gameObject);
        gm.SetActive(false);
        prepareObject(gm);
        pool.Add(gm);
    }

    public Bullet getNew(Vector3 position, Quaternion rotation)
    {
        Bullet gm = Instantiate(prefab, position, rotation);
        NetworkServer.Spawn(gm.gameObject);
        prepareObject(gm);
        pool.Capacity++;
        pool.Add(gm); 
        gm.SetActive(true);
        return gm;
    }

    public void prepareObject(Bullet obj)
    {
        obj.transform.parent = transform;
    }

    void IncreasePool()
    {
        pool.Capacity *= 2;
        for (int i = nrOfActiveObjects; i < pool.Capacity; i++)
        {
            createNewObject();
        }
    }

    public Bullet RetrieveInstance(Vector3 position, Quaternion rotation)
    {
        if (nrOfActiveObjects >= pool.Capacity)
            IncreasePool();

        var currentBullet = pool[nrOfActiveObjects];
        currentBullet.PoolIndex = nrOfActiveObjects;
        currentBullet.SetActive(true);
        currentBullet.transform.position = position;
        currentBullet.transform.rotation = rotation;


        nrOfActiveObjects += 1;
        Debug.Log(currentBullet);
        return currentBullet;
    }
    
    public void DestroyBullet(Bullet b)
    {
        b.SetActive(false);
        nrOfActiveObjects -= 1;
        pool[b.PoolIndex] = pool[nrOfActiveObjects];
        pool[nrOfActiveObjects] = b;
    }
}
