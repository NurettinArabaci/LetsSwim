using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerMovement : Player
{

    public static Vector3 currentPose;



     protected void Update()
     {
         if (isActiveGame)
         {
             StartMovement();

             BreathControl();

             currentPose = mT.position;
         }
     }
    


    protected virtual void StartMovement()
    {
        if (Input.GetMouseButtonDown(0))
        {
            upMovement = false;
            breathing = false;

            Enemy.enemyMove = true;

            CamManager.instance.OnStartGame();

            BubbleFxActive(true);



            if (isSurface)
            {
                mT.position += Vector3.forward + Vector3.down / 4;
                ResetAnim(AnimParam.idle);
                mAnim.SetTrigger(AnimParam.swim);
                isSurface = false;
            }
            mT.DOLookAt(mT.position + Vector3.forward + Vector3.down / 4,0.5f);

        }

        else if (Input.GetMouseButton(0))
        {

            mT.position += (Vector3.forward + Vector3.down / 4) * Time.deltaTime * speed;
            mT.position = Vector3.up*Mathf.Clamp(mT.position.y, -limitY, limitY)+Vector3.forward*mT.position.z;
            

            DecreaseBreath();

        }

        else if (Input.GetMouseButtonUp(0))
        {
            upMovement = true;

            breathing = true;

            mT.DOLookAt(mT.position + Vector3.forward + Vector3.up / 3,0.5f);

        }

        if (upMovement)
        {
            mT.position += (Vector3.forward + Vector3.up / 3) * Time.deltaTime * speed;
            

        }

        if (breathing)
        {
            IncreaseBreath();
        }


    }

    
   
    


}
