using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{

    public static int StaminaPrice
    {
        get { return PlayerPrefs.GetInt("StaminaPrice", 30); }
        set { PlayerPrefs.SetInt("StaminaPrice", value); }
    }
    public static int StaminaLevel
    {
        get { return PlayerPrefs.GetInt("StaminaLevel", 1); }
        set { PlayerPrefs.SetInt("StaminaLevel", value); }
    }

    public static int IncomePrice
    {
        get { return PlayerPrefs.GetInt("IncomePrice", 30); }
        set { PlayerPrefs.SetInt("IncomePrice", value); }
    }
    public static int IncomeLevel
    {
        get { return PlayerPrefs.GetInt("IncomeLevel", 1); }
        set { PlayerPrefs.SetInt("IncomeLevel", value); }
    }



    protected bool EnoughMoney(float price)
    {
        if (Player.Wallet > price)
        {
            return true;
        }

        return false;
    }
}

public enum UpgradeType { Stamina, Income }
