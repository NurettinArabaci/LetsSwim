using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class StartButton : MonoBehaviour, IPointerDownHandler
{
    //Button button;

    private void Awake()
    {
       // button=GetComponent<Button>();
        //button.onClick.AddListener(StartButtonRun);
    }



    public void StartButtonRun()
    {
        Player.isActiveGame = true;

        EventManager.Fire_OnPlayerMove();

        transform.parent.gameObject.SetActive(false);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        StartButtonRun();
    }
}
