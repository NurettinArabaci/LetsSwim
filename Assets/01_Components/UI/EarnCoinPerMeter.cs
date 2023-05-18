using System;
using UnityEngine;
using TMPro;

public class EarnCoinPerMeter : MonoBehaviour
{
    TextMeshProUGUI _text;
    string coinAmount;
    private void Awake()
    {
        coinAmount = String.Format("{0:0.0}", Math.Round(Player.CoinIncrease, 1));
        _text = GetComponent<TextMeshProUGUI>();
        _text.SetText($"+ {coinAmount}$");
    }
}
