using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var colls = GetComponentsInChildren<Collectible>();
        if (colls.Length != 1) print(colls.Length);
        foreach(Collectible coll in colls)
        {
            coll.gameController = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateFlag(Collider player) 
    {
        ;
    }
}
