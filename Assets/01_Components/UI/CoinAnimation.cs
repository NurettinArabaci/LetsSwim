using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class EventManager
{
    public static event Action OnCoinAnimation;
    public static void Fire_OnCoinAnimation() { OnCoinAnimation?.Invoke(); }
}

public class CoinAnimation : MonoBehaviour
{
    [SerializeField] Animator anim;

    private void OnEnable()
    {
        EventManager.OnCoinAnimation += EventManager_OnCoinAnimation;
    }

    private void EventManager_OnCoinAnimation()
    {
        anim.SetTrigger("run");
    }

    private void OnDisable()
    {
        EventManager.OnCoinAnimation -= EventManager_OnCoinAnimation;
    }
}
