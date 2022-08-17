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

            //CheckPosition();

            upMovement = false;

            //BubbleFxActive(false);

            //AnimationChanging(AnimParam.swim, AnimParam.idle);


        }

        else if (other.CompareTag(Tags.Enemy))
        {
            Enemy enemy = other.GetComponent<Enemy>();

            enemy.EnemyAttack();

            Die();

        }

        else if (other.CompareTag("StartPoint"))
        {
            BubbleFxActive(false);
            rb.velocity = rb.velocity.normalized * 10;
            AnimationChanging(AnimParam.swim, AnimParam.standUp);
            isActiveGame = false;
            breathing = true;

        }

        else if (other.CompareTag("EndPoint"))
        {
            mAnim.SetTrigger(AnimParam.dive);
            rb.velocity = rb.velocity.normalized * 7;
            isActiveGame = true;

        }
    }
    
}
