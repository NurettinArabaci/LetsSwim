using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollider : PlayerMovement
{
    

    protected override void Awake()
    {
        base.Awake();

        BubbleFxActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Tags.Water))
        {
            upMovement = false;
            isSurface = true;

            mAnim.SetTrigger(AnimParam.idle);
            ResetAnim(AnimParam.swim);

            CheckPosition();

            BubbleFxActive(false);

        }
    }

    

    void CheckPosition()
    {
        if (mT.position.y > 0)
            mT.position = mT.position.z * Vector3.forward;
        
    }
}
