using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Shoulder : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 1;
    // Start is called before the first frame update
    void Start()
    {
    }

    public void RotateArm(float vert)
    {
        var zAngle = vert * rotationSpeed;

        var newZ = transform.rotation.eulerAngles.z + zAngle;

        if (newZ >= -60 && newZ < 90 || newZ > 300)
        {
            transform.Rotate(new Vector3(0f, 0f, zAngle));
        }
    }
}
