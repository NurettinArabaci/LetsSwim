using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Meters : MonoBehaviour
{
    TextMeshPro meterText;

    private void Awake()
    {
        meterText = GetComponent<TextMeshPro>();
    }
    void Start()
    {
        meterText.text = (transform.parent.localPosition.z).ToString()+"m";
    }

    
}
