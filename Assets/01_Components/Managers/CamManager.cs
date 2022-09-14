using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


public class CamManager : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera cam;

    public static CamManager instance;

    private void Awake()
    {
        instance=this;
    }

    public void OnStartGame()
    {
        cam.m_Priority = 9;
    }
    public void OnFinishGame()
    {
        cam.m_Priority = 11;
    }

}
