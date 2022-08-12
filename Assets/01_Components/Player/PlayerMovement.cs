using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

            CamManager.instance.OnStartGame();

            BubbleFxActive(true);



            if (isSurface)
            {
                mT.position += Vector3.down / 4;
                ResetAnim(AnimParam.idle);
                mAnim.SetTrigger(AnimParam.swim);
                isSurface = false;
            }

        }

        else if (Input.GetMouseButton(0))
        {

            mT.position += (Vector3.forward + Vector3.down / 4) * Time.deltaTime * speed;
            mT.position = new Vector3(0, Mathf.Clamp(mT.position.y, -limitY, limitY), mT.position.z);

            DecreaseBreath();

        }

        else if (Input.GetMouseButtonUp(0))
        {
            upMovement = true;

            breathing = true;

        }

        if (upMovement)
        {
            mT.position += (Vector3.forward + Vector3.up / 2) * Time.deltaTime * speed;


        }

        if (breathing)
        {
            IncreaseBreath();
        }


    }
}
