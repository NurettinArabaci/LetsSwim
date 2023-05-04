using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class StartButton : MonoBehaviour
{
    Button button;

    private void Awake()
    {
        button=GetComponent<Button>();
        button.onClick.AddListener(StartButtonRun);
    }



    public void StartButtonRun()
    {
        Player.isActiveGame = true;

        EventManager.Fire_OnPlayerMove();

        //UIManager.instance.upgradePanel.SetActive(false);
        transform.parent.gameObject.SetActive(false);
    }
}
