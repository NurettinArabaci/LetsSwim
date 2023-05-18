using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CoinSpawner : MonoSingleton<CoinSpawner>
{
    [SerializeField] private EarnCoinPerMeter earnCoinPerMeter;

    [SerializeField] private RectTransform initPos, endPos;

    public void SpawnEarnCoin()
    {
        EarnCoinPerMeter _earnCoinPerMeter = Instantiate(earnCoinPerMeter, initPos.position, Quaternion.identity, initPos);

        _earnCoinPerMeter.transform.DOMove(endPos.position, 1.5f).OnComplete(() => Destroy(_earnCoinPerMeter.gameObject));
    }
}
