using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Unity.Mathematics;

public class Shoulder : MonoBehaviour
{
    float angle = 90;
    public void RotateArm(float vert, float hor)
    {
        vert = math.round(vert);
        hor= math.round(hor);
        hor = math.abs(hor);
        angle = 90 + (vert * 90);
        angle += (45 * hor * vert * -1);
        
        transform.localRotation = Quaternion.Euler(0, 0, angle);
    }
}
