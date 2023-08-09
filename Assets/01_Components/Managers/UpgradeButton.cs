using System;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class UpgradeButton : UpgradeManager
{
    public UpgradeType upgradeType;
    [SerializeField] TextMeshProUGUI upgradeLevelText;
    [SerializeField] TextMeshProUGUI priceText;

    Button button;

    public int UpgradeLevel
    {
        get
        {
            if (upgradeType == UpgradeType.Stamina)
                return StaminaLevel;
            return IncomeLevel;
        }
        set
        {
            if (upgradeType == UpgradeType.Stamina)
                StaminaLevel=value;
            else
                IncomeLevel=value;
        }
    }

    public int UpgradePrice
    {
        get
        {
            if (upgradeType == UpgradeType.Stamina)
                return StaminaPrice;
            return IncomePrice;
        }
        set
        {
            if (upgradeType == UpgradeType.Stamina)
                StaminaPrice = value;
            else
                IncomePrice = value;
        }
    }

    private void Awake()
    {
        button = GetComponentInChildren<Button>();

        
        button.onClick.AddListener(UpgradeProcess);

        EventManager.OnCheckMoney += OnCheckMoney;

        EventManager.Fire_OnCheckMoney();
    }

    private void Start()
    {
        UpdateTexts(UpgradePrice.ToString() + "$", "lvl " + UpgradeLevel.ToString());
    }

    public void UpdateTexts(string _priceText, string _upgradeLevelText)
    {
        priceText.text = _priceText;
        upgradeLevelText.text = _upgradeLevelText;
        UIManager.instance.coin.text = String.Format("{0:0.0}", Math.Round(Player.Wallet, 1));
    }

    public void OnCheckMoney()
    {
         button.interactable = Player.Wallet > UpgradePrice;
    }


    public void StaminaOrIncome()
    {

        switch (upgradeType)
        {
            case UpgradeType.Stamina:
                Player.Stamina *= 0.9f;
                StaminaPrice = (int)(StaminaPrice * 1.5f);
                break;

            case UpgradeType.Income:
                Player.CoinIncrease += 0.1f;
                IncomePrice = (int)(IncomePrice * 1.8f);
                break;
          
        }
    }

    public void UpgradeProcess()
    {
        if (EnoughMoney(UpgradePrice))
        {

            Player.Wallet -= UpgradePrice;
            StaminaOrIncome();
            UpgradeLevel++;
            UpdateTexts(UpgradePrice.ToString() + "$", "lvl " + UpgradeLevel.ToString());

            EventManager.Fire_OnPlayVfxOneShot(VfxType.Upgrade);
            EventManager.Fire_OnCoinAnimation();
            EventManager.Fire_OnCheckMoney();
        }
        else
        {
            EventManager.Fire_OnCheckMoney();
            print(" open adds or not enough money");
        }
    }

    private void OnDisable()
    {
        EventManager.OnCheckMoney -= OnCheckMoney;
    }

}
