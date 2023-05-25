using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Enemy : MonoBehaviour
{
    #region SerializeFields
    [SerializeField] Transform followObj;
    [SerializeField] Transform getParent;
    #endregion

    #region Private Fields
    private Transform mT;

    private Vector3 target;

    private Vector3 offset;

    private Rigidbody rb;

    private Animator anim;

    private float Speed
    {
        get => Vector3.Distance(followObj.position, mT.position) >= 9 ? 10 : 9.3f;
    }
    #endregion

    private bool enemyMove;



    private void Awake()
    {
        References();

        EventManager.OnPlayerMove += () => enemyMove = true;

        offset = Vector3.back * 2 + Vector3.up * 1.4f;
    }


    private void FixedUpdate()
    {
        if (!enemyMove)
            return;

        Movement();


    }

    void Movement()
    {
        target = Vector3.MoveTowards(mT.position, followObj.position + offset, Speed * Time.fixedDeltaTime);
        rb.MovePosition(target);
        mT.LookAt(followObj.position + Vector3.up);


    }

    public async void PlayerGetParent(Transform player)
    {
        await System.Threading.Tasks.Task.Delay(150);
        player.parent = getParent;
    }

    public void EnemyAttack()
    {
        anim.SetTrigger(AnimParam.attack);
        enemyMove = false;

        GetComponent<Collider>().enabled = false;

        mT.DOMove(new Vector3(0, 2f, mT.position.z + 3.5f), 1f);
        mT.DOLookAt(new Vector3(0, 2f, mT.position.z + 3f), 1f);

        StartCoroutine(MoveAfterFeeding());
    }

    public void EatRestRoad(EatRestRoad eatRestRoad)
    {
        eatRestRoad.EatRoad();

        anim.SetTrigger("boxAttack");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out EatRestRoad eatRestRoad))
        {
            EatRestRoad(eatRestRoad);
        }


    }



    void References()
    {
        mT = transform;

        rb = GetComponent<Rigidbody>();

        anim = GetComponentInChildren<Animator>();
    }

    IEnumerator MoveAfterFeeding()
    {
        EventManager.Fire_OnPlayVfxOneShot(VfxType.GameEnd);
        yield return new WaitForSeconds(0.5f);
        Vector3 movePose = mT.position + (Vector3.down + Vector3.forward * 5) * 5;
        mT.DOLookAt(movePose, 1);
        yield return new WaitForSeconds(0.2f);

        mT.DOMove(movePose, 2);

    }

    private void OnDisable()
    {
        EventManager.OnPlayerMove -= () => enemyMove = true;
    }

}
