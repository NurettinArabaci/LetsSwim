using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Enemy : MonoBehaviour
{
    Transform mT;

    Vector3 target;

    Vector3 offset;

    float speed;

    Rigidbody rb;

    public static bool enemyMove;

    Animator enemyAnim;

    private void Awake()
    {
        References();

        speed = 9;

        offset = Vector3.back*2+Vector3.up;
    }


    private void FixedUpdate()
    {
        if (!enemyMove)
            return;

        Movement();
        

    }

    void Movement()
    {
        target = Vector3.MoveTowards(mT.position, PlayerMovement.currentPose +offset , speed * Time.fixedDeltaTime);
        rb.MovePosition(target);
        mT.LookAt(PlayerMovement.currentPose + Vector3.up);

        speed = Vector3.Distance(PlayerMovement.currentPose, mT.position) >= 9 ? 10 : 9.3f;

    }

    public void EnemyAttack()
    {
        enemyAnim.SetTrigger(AnimParam.attack);
        enemyMove = false;
        mT.DOMove(new Vector3(0,1.6f, mT.position.z+2f), 1f);
        mT.DOLookAt(new Vector3(0, 1.6f, mT.position.z + 1.5f), 1f);

        StartCoroutine(MoveAfterFeeding());
    }

    void References()
    {
        mT = transform;

        rb = GetComponent<Rigidbody>();

        enemyAnim = GetComponentInChildren<Animator>();
    }

    IEnumerator MoveAfterFeeding()
    {
        yield return new WaitForSeconds(0.5f);
        Vector3 movePose = mT.position + (Vector3.down + Vector3.forward * 5) * 4;
        mT.DOLookAt(movePose, 1);
        yield return new WaitForSeconds(0.2f);
        
        mT.DOMove(movePose,2);
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("StartPoint"))
        {
            enemyMove = false;

            rb.AddForce((Vector3.forward + Vector3.down / 6)*500);


        }

        else if (other.CompareTag("EndPoint"))
        {
            enemyMove = true;
            rb.velocity =Vector3.zero;

        }
    }
}
