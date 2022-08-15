using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Enemy : MonoBehaviour
{
    Transform mT;

    Vector3 target;

    Vector3 offset;

    int speed;

    Rigidbody rb;

    public static bool enemyMove;

    Animator enemyAnim;

    private void Awake()
    {
        References();

        speed = 9;

        offset = Vector3.back*2+Vector3.up;
    }


    private void LateUpdate()
    {
        if (!enemyMove)
            return;

        Movement();

    }

    void Movement()
    {
        target = Vector3.MoveTowards(mT.position, PlayerMovement.currentPose +offset , speed * Time.deltaTime);
        rb.MovePosition(target);
        transform.LookAt(PlayerMovement.currentPose + Vector3.up);
    }

    public void EnemyAttack()
    {
        enemyAnim.SetTrigger(AnimParam.attack);
        enemyMove = false;
        mT.DOMoveZ(mT.position.z+1.5f, 0.5f);

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

        Vector3 movePose = mT.position + (Vector3.down + Vector3.forward*3) * 10;
        mT.DOMove(movePose,10);
        mT.DOLookAt(movePose, 1);
    }
}
