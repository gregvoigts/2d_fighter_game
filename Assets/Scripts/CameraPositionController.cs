using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CameraPositionController : MonoBehaviour
{

    public float speedX = 0.9f;
    public float speedY = 0.9f;
    public float speed = 0.2f;
    [Range(1, 25)]
    public float MinDis = 5;
    [Range(1, 25)]
    public float MaxDis = 15;
    private List<GameObject> players;

    private LevelBounds bounds;
    // Start is called before the first frame update
    void Start()
    {
        bounds = GameObject.FindObjectOfType<LevelBounds>();
    }

    // Update is called once per frame
    void Update()
    { 
        // TODO: find players when they are spawned
        players = GameObject.FindGameObjectsWithTag("Player").ToList();
        if(players.Any())
            UpdateCameraPosition();
    }

    private void UpdateCameraPosition()
    {
        var Xcord = players.Select(p => p.transform.position.x);
        var Ycord = players.Select(p => p.transform.position.y);
        // Calculate X center
        float difX;
        var centerX = CalculateCenter(Xcord.Max(), Xcord.Min(), out difX);
        // Calculate Y center
        float difY;
        var centerY = CalculateCenter(Ycord.Max(), Ycord.Min(), out difY);        
        // projection Size needed to see all players
        float projectionSize = Mathf.Clamp(difX > difY * (16/9) ? difX * 0.55f : difY * 0.75f, MinDis, MaxDis);
        //offset center
        float minCenter = bounds.Height.x + projectionSize;
        var center = Mathf.Max(minCenter, centerY);
        // apply
        Camera.main.transform.position = new Vector3(
            Transition(Camera.main.transform.position.x, centerX, speedX), 
            Transition(Camera.main.transform.position.y, center, speedY), 
            -10);
        Camera.main.orthographicSize = Transition(Camera.main.orthographicSize, projectionSize, speed);
    }

    private float Transition(float val, float nextVal, float duration){
        return Mathf.Lerp(val, nextVal, duration);
    }

    private float CalculateCenter(float max, float min, out float dif) {
        dif = Mathf.Abs(max - min);
        return dif * 0.5f + min;
    }
}
