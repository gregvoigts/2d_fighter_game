using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarInner : MonoBehaviour
{
    public float FillAmount { set { transform.localScale = new Vector3(value,1); } }
    // Start is called before the first frame update
    void Start()
    {
       
    }
}
