using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum UpgradeType { Stamina, Income}
public class Upgrade : UpgradeManager
{
    public UpgradeType upgradeType;
    [SerializeField] TextMeshProUGUI upgradeLevelText;
    [SerializeField] TextMeshProUGUI priceText;

    Button button;

    private void Awake()
    {
        button = GetComponentInChildren<Button>();
        if (upgradeType==UpgradeType.Stamina)
        {
            UpdateTexts(StaminaPrice.ToString() + "$", "lv " + StaminaLevel.ToString());
            button.onClick.AddListener(StaminaUpgrade);
        }
        else if (upgradeType == UpgradeType.Income)
        {
            UpdateTexts(IncomePrice.ToString() + "$", "lv " + IncomeLevel.ToString());
            button.onClick.AddListener(IncomeUpgrade);
        }
    }


    public void UpdateTexts(string _priceText, string _upgradeLevelText)
    {
        priceText.text = _priceText;
        upgradeLevelText.text = _upgradeLevelText;
        UIManager.instance.coin.text = String.Format("{0:0.0}", Math.Round(Player.Wallet, 1));
    }

    public void StaminaUpgrade()
    {
        if (EnoughMoney(StaminaPrice))
        {
            Player.Stamina *= 0.9f;
            StaminaPrice = (int)(StaminaPrice * 1.5f);
            StaminaLevel++;
            UpdateTexts(StaminaPrice.ToString() + "$", "lv "+StaminaLevel.ToString());
        }
        else
            print("open adds or not enough money");

    }

    public void IncomeUpgrade()
    {
        if (EnoughMoney(IncomePrice))
        {
            Player.CoinIncrease += 0.1f;
            IncomePrice = (int)(IncomePrice * 1.8f);
            IncomeLevel++;
            UpdateTexts(IncomePrice.ToString() + "$", "lv "+IncomeLevel.ToString());
        }
        else
            print("open adds or not enough money");

    }
}
