using System;
using UnityEngine;
using TMPro;

public class EarnCoinPerMeter : MonoBehaviour
{
    TextMeshPro textMeshPro;
    private void Awake()
    {
        textMeshPro = GetComponent<TextMeshPro>();
        textMeshPro.text = "+" + String.Format("{0:0.0}", Math.Round(Player.CoinIncrease, 1))  + "$";
    }
}
