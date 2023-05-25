using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Meters : MonoBehaviour
{
    TextMeshPro meterText;

    private void Awake()
    {
        meterText = GetComponentInChildren<TextMeshPro>();
    }
    void Start()
    {
        meterText.text = (transform.localPosition.z*100).ToString()+"m";
    }

    
}
