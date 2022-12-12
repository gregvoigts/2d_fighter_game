using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arm : MonoBehaviour
{
    [SerializeField] RangeWeapon gunPrefab;
    // Start is called before the first frame update
    void Start()
    {
        RangeWeapon mW = Instantiate(gunPrefab);
        mW.transform.SetParent(transform);
        mW.gameObject.SetActive(true);
        mW.transform.localPosition = new Vector3(0, -0.4f, 0);        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
