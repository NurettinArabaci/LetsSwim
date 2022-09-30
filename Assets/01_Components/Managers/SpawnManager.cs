using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private EarnCoinPerMeter earnCoinPerMeter;

    public static SpawnManager instance;

    private void Awake()
    {
        instance = this;
    }

    public void SpawnEarnCoin(Vector3 pos)
    {
        EarnCoinPerMeter _earnCoinPerMeter= Instantiate(earnCoinPerMeter,pos,earnCoinPerMeter.transform.localRotation);
        Destroy(_earnCoinPerMeter.gameObject, 1f);
    }

}
