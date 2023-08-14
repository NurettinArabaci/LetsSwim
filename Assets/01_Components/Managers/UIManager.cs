using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.SceneManagement;
using System;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [SerializeField] private GameObject startPanel;
    [SerializeField] private TextMeshProUGUI distanceAmount;
    [SerializeField] private GameObject inGamePanel;

    public TextMeshProUGUI coin;
    public TextMeshProUGUI scoreAmount;
    public TextMeshProUGUI highScoreAmount;
    public TextMeshProUGUI bestScoreText;
    public GameObject newHighScoreText;

    public GameObject failedPanel;
    public GameObject upgradePanel;

    private void Awake()
    {
        instance = this;
        
        CoinChange(0);

    }

    private void Start()
    {
        //        startPanel.SetActive(true);

        EventManager.OnPlayerMove += PanelOpen;
        failedPanel.SetActive(false);
        inGamePanel.SetActive(false);
        
    }

    void PanelOpen()
    {
        inGamePanel.SetActive(true);
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }

    public void OpenRestartPanel()
    {
        coin.transform.parent.gameObject.SetActive(false);
        failedPanel.SetActive(true);
        failedPanel.transform.localScale = Vector3.zero;
        failedPanel.transform.DOScale(1, 0.3f);
    }

    public void CoinChange(int distance)
    {
        coin.text = MoneyText();
      
        distanceAmount.text = distance.ToString();
        bestScoreText.text = ScoreManager.HighScore.ToString() + " m";

    }

    string MoneyText()
    {
        var coinAmount = Player.Wallet + Player.CoinTemp;
        if (coinAmount > 999 && coinAmount < 1000000)
        {
            return $"{String.Format("{0:0.00}", Math.Round((float)coinAmount / 1000, 2))}K $";
        }
        else if (coinAmount >= 1000000 && coinAmount < 1000000000)
        {
            return $"{String.Format("{0:0.00}", Math.Round((float)coinAmount / 1000000, 2))}M $";
        }

        return coinAmount.ToString("0.0") + " $";
    }

    private void OnDisable()
    {
        EventManager.OnPlayerMove -= PanelOpen;
    }


}
