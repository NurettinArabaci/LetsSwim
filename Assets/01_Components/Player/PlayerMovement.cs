using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerMovement : Player
{

    public static Vector3 currentPose;
    Vector3 initPose;

    bool isFinishPressed = true;
    

    private void Start()
    {
        isDeath = false;
        initPose = mT.position;
    }
    //bool move = false;

    private void FixedUpdate()
    {
        if (isDeath) return;
        
        if (isActiveGame)
        {
            Movement();
        }

        currentPose = mT.position;
        DistanceCalculate();
        BreathControl();

        if (breathing)
        {
            IncreaseBreath();
        }

    }


    

    int _distance = 0;
    void DistanceCalculate()
    {
        _distance = (int)Vector3.Distance(currentPose, initPose);
        CoinTemp = _distance * CoinIncrease;

        UIManager.instance.CoinChange(_distance);
    }

    protected virtual void Movement()
    {
        if (Input.GetMouseButtonDown(0))
        {
            upMovement = false;
            breathing = false;

            Enemy.enemyMove = true;

            CamManager.instance.OnStartGame();

            BubbleFxActive(true);

            rb.drag = 0f;

           // move = true;

        }

        else if (Input.GetMouseButton(0))
        {
            if (!isFinishPressed) return;
            if (rb.velocity.magnitude <= 10)
            {
                rb.AddForce(Vector3.forward * 500 * Time.fixedDeltaTime, ForceMode.Acceleration);
            }

            AnimationChanging(AnimParam.idle, AnimParam.swim);

            DecreaseBreath();

        }

        else if (Input.GetMouseButtonUp(0))
        {

            rb.drag = 0.5f;

            breathing = true;

            StartCoroutine(PressControl());

        }


        if (rb.velocity.magnitude > 10)
        {
            rb.velocity = rb.velocity.normalized * 10;
        }
        else if (rb.velocity.magnitude > 5)
        {
            mAnim.speed = rb.velocity.magnitude / 10;
        }
        else
            rb.velocity = rb.velocity.normalized * 5;


    }

    IEnumerator PressControl()
    {
        isFinishPressed = false;
        yield return new WaitForSeconds(1f);
        isFinishPressed = true;
    }

}
