using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    HealthBarInner healthBarInner;
    // Start is called before the first frame update
    void Start()
    {
        healthBarInner = GetComponentInChildren<HealthBarInner>();
    }

    public void onHealthChanged(float healthProcentage)
    {
        Debug.Log("Health chnage: " + healthProcentage);
        Debug.Log(healthBarInner);
        healthBarInner.FillAmount = healthProcentage;
    }
}
