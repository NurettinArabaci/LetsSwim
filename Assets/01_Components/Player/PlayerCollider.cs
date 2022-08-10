using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollider : Player
{
    protected override void Awake()
    {
        base.Awake();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tags.Water))
        {
            upMovement = false;

            mAnim.SetTrigger(AnimParam.idle);
            isSwim = true;

            ResetAnim(AnimParam.swim);

            CheckPosition();

            BubbleFxActive(false);

            print("Test PlayerCollider");

        }
    }

    void CheckPosition()
    {
        if (mT.position.y > 0)
            mT.position = mT.position.z * Vector3.forward;
        
    }
}
