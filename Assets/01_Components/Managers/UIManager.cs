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
    [SerializeField] public GameObject failedPanel;
    [SerializeField] public GameObject upgradePanel;
    [SerializeField] public TextMeshProUGUI coin;
    [SerializeField] TextMeshProUGUI distanceAmount;

    [SerializeField] public TextMeshProUGUI scoreAmount;
    [SerializeField] public TextMeshProUGUI highScoreAmount;
    [SerializeField] public TextMeshProUGUI bestScoreText;
    [SerializeField] public GameObject newHighScoreText;

    private void Awake()
    {
        instance = this;
        
        CoinChange(0);

    }

    private void Start()
    {
        startPanel.SetActive(true);
        failedPanel.SetActive(false);
        
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
        double x = Player.Wallet + Player.CoinTemp;
        coin.text = String.Format("{0:0.0}", Math.Round(x, 1));
      
        distanceAmount.text = distance.ToString();
        bestScoreText.text = ScoreManager.HighScore.ToString() + " m";

    }

    void DestroyPanel()
    {
        Debug.Log("Destroy Panel");
        Destroy(failedPanel.gameObject);
    }

}
