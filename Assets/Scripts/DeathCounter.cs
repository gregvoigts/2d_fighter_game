using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;

public class DeathCounter : MonoBehaviour
{
    [SerializeField] TMP_Text txtField;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    public void UpdateCounter(float value)
    {
        txtField.text = math.ceil(value).ToString();
    }
}
