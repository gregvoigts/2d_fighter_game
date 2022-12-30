using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LevelBounds : MonoBehaviour
{
    private Tilemap tm;
    public Vector2 Width {get;private set;}
    public Vector2 Height {get;private set;}
    public Vector2 OffsetHeight;
    // Start is called before the first frame update
    void Start()
    {
        // var x = transform.GetComponentsInChildren<Transform>();
        tm = GetComponent<Tilemap>();
        tm.CompressBounds();
        print(tm.localBounds.extents);
        Width = new Vector2(tm.localBounds.center.x - tm.localBounds.extents.x, tm.localBounds.center.x + tm.localBounds.extents.x);
        Height = new Vector2(tm.localBounds.center.y - tm.localBounds.extents.y + OffsetHeight.x, tm.localBounds.center.y + tm.localBounds.extents.y - OffsetHeight.y);
    
    }

}
