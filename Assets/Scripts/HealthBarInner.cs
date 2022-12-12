using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarInner : MonoBehaviour
{
    private Image healthImage;

    public float FillAmount { set { healthImage.fillAmount = value; } }
    // Start is called before the first frame update
    void Start()
    {
        healthImage= GetComponent<Image>();
    }
}
