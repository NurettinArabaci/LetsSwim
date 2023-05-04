using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : Player
{
    Vector3 initPose;
    [SerializeField] Transform earnCoinSpawnTransform;

    bool isFinishPressed = true;

    private void Start()
    {
        initPose = mT.position;
        Distance = 0;
        EventManager.OnPlayerMove += OnMove;
    }

    void OnMove()
    {
        GetComponent<Rigidbody>().AddForce(Vector3.forward * 500 * Time.fixedDeltaTime, ForceMode.Acceleration);
        AnimationChanging(AnimParam.idle, AnimParam.swim);

        rb.drag = 0f;
    }



    private void FixedUpdate()
    {
        if (isFinish) return;
        
        if (isDeath()) return;
        
        if (isActiveGame)
        {
            Movement();
        }

        DistanceCalculate();
        

        if (breathing)
        {
            IncreaseBreath();
        }

    }




    int tempDistance = 0;
    int multiplyDistance = 0;
    void DistanceCalculate()
    {
        

        Distance = (int)Vector3.Distance(mT.position, initPose);
        CoinTemp = Distance * CoinIncrease;

        if (tempDistance != Distance)
        {
            if (Distance%3==0 && Distance>9 && multiplyDistance!=Distance)
            {
                SpawnManager.instance.SpawnEarnCoin(earnCoinSpawnTransform.position);
                multiplyDistance = Distance;
            }
            
            tempDistance = Distance;
            UIManager.instance.CoinChange(Distance);
        }

        
    }

    protected virtual void Movement()
    {
        if (Input.GetMouseButtonDown(0))
        {
            upMovement = false;
            breathing = false;

            

            BubbleFxActive(true);

            rb.drag = 0f;

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

    private void OnDisable()
    {
        EventManager.OnPlayerMove -= OnMove;
    }

}
