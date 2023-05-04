using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


public class CamManager : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera beginCam;
    [SerializeField] CinemachineVirtualCamera playCam;

    public static CamManager instance;

    private void Awake()
    {
        instance=this;

        EventManager.OnPlayerMove += OnStartGame;
    }

    public void OnStartGame()
    {
        beginCam.m_Priority = 9;
        
    }
    public void OnFinishGame()
    {
        //playCam.Follow = null;
        beginCam.m_Priority = 11;
    }

    private void OnDisable()
    {
        EventManager.OnPlayerMove -= OnStartGame;
    }


}
