using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerMovement : Player
{

    public static Vector3 currentPose;

    //bool isSwim = false;
    //bool buttonUp = true;

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

    /*protected void Update()
     {
         if (isActiveGame)
         {
             Movement();

             BreathControl();

             currentPose = mT.position;
         }
     }*/

    
    
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
            rb.AddForce(Vector3.forward * 12000 * Time.fixedDeltaTime);

        }

        else if (Input.GetMouseButton(0))
        {

            AnimationChanging(AnimParam.idle, AnimParam.swim);

            //mT.position += Vector3.forward * Time.deltaTime * speed;
            //mT.position = Vector3.up*Mathf.Clamp(mT.position.y, -limitY, limitY)+Vector3.forward*mT.position.z;

            DecreaseBreath();
            print(rb.velocity.magnitude);

        }

        else if (Input.GetMouseButtonUp(0))
        {
            //upMovement = true;

            //rb.AddForce(Vector3.back * 100);
            //rb.velocity = rb.velocity.normalized * 6;
            rb.drag = 0.5f;

            breathing = true;

            //mT.DOLookAt(mT.position + (Vector3.forward + Vector3.up / 3), 0.3f);

            
        }

        
        if (rb.velocity.magnitude > 10)
        {
            rb.velocity = rb.velocity.normalized * 10;
        }
        else if (rb.velocity.magnitude > 5)
        {
            mAnim.speed = rb.velocity.magnitude / 10;
        }

        
    }
   
}
