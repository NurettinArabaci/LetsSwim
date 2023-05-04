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
            UpdateTexts(StaminaPrice.ToString() + "$", "lvl " + StaminaLevel.ToString());
            button.onClick.AddListener(StaminaUpgrade);
            if (Player.Wallet < StaminaPrice) button.interactable = false;
        }
        if (upgradeType == UpgradeType.Income)
        {
            UpdateTexts(IncomePrice.ToString() + "$", "lvl " + IncomeLevel.ToString());
            button.onClick.AddListener(IncomeUpgrade);
            if (Player.Wallet < IncomePrice) button.interactable = false;
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
            UpdateTexts(StaminaPrice.ToString() + "$", "lvl "+StaminaLevel.ToString());

            EventManager.Fire_OnPlayVfxOneShot(VfxType.Upgrade);

            if (!EnoughMoney(StaminaPrice)) button.interactable = false;
        }
        else
        {
            button.interactable = false;
            print(" open adds or not enough money");
        }
            
       
    }

    public void IncomeUpgrade()
    {
        if (EnoughMoney(IncomePrice))
        {
            Player.CoinIncrease += 0.1f;
            IncomePrice = (int)(IncomePrice * 1.8f);
            IncomeLevel++;
            UpdateTexts(IncomePrice.ToString() + "$", "lvl "+IncomeLevel.ToString());

            EventManager.Fire_OnPlayVfxOneShot(VfxType.Upgrade);

            if (!EnoughMoney(IncomePrice)) button.interactable = false;
        }
        else
        {
            button.interactable = false;
            print(" open adds or not enough money");
        }
    }
}
