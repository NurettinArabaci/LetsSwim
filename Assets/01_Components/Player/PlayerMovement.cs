using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerMovement : Player
{

    public static Vector3 currentPose;

    private void FixedUpdate()
    {
        if (isActiveGame)
        {
            Movement();

            currentPose = mT.position;
        }


        BreathControl();
        if (breathing)
        {
            IncreaseBreath();
        }

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

        }

        else if (Input.GetMouseButton(0))
        {
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

}
