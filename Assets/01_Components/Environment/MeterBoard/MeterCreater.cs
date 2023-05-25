using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeterCreater : MonoBehaviour
{
    [SerializeField] Meters meters;


    private void Awake()
    {

        for (int i = 0; i < 200; i++)
        {
            Instantiate(meters, transform.position+Vector3.forward * i*50, Quaternion.identity, transform);
        }
    }
}
