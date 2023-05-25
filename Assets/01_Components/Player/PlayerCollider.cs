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
        if (other.TryGetComponent(out Enemy enemy))
        {
            enemy.EnemyAttack();
            enemy.PlayerGetParent(mesh.transform.parent);
            breath = 0;

        }


        if (other.CompareTag("StartPoint"))
        {
            EnterRestRoad();

        }

        if (other.CompareTag("EndPoint"))
        {
            ExitRestRoad();

        }
    }


    void EnterRestRoad()
    {
        BubbleFxActive(false);
        AnimationChanging(AnimParam.swim, AnimParam.standUp);

        rb.drag = 0f;
        rb.velocity = rb.velocity.normalized * 10;

        isActiveGame = false;
        breathing = true;

        mAnim.speed = rb.velocity.magnitude / 10;
    }

    void ExitRestRoad()
    {
        mAnim.SetTrigger(AnimParam.dive);

        rb.drag = 0.2f;
        isActiveGame = true;
    }
   
}
