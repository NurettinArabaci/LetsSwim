using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [SerializeField] private GameObject startPanel;
    [SerializeField] public GameObject failedPanel;
    [SerializeField] public GameObject upgradePanel;
    [SerializeField] TextMeshProUGUI coin;
    [SerializeField] TextMeshProUGUI distanceAmount;

    private void Awake()
    {
        instance = this;
        failedPanel.transform.localScale=Vector3.zero;
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
        failedPanel.transform.DOScale(1, 0.3f);
    }

    public void CoinChange(int distance)
    {
        coin.text = (Player.Coin+Player.CoinTemp).ToString();
        distanceAmount.text = distance.ToString();

    }
}
