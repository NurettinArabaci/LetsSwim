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
        if (other.CompareTag(Tags.Enemy))
        {
            Enemy enemy = other.GetComponent<Enemy>();

            enemy.EnemyAttack();

            breath = 0;

        }

        else if (other.CompareTag("StartPoint"))
        {
            EnterRestRoad();

        }

        else if (other.CompareTag("EndPoint"))
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
