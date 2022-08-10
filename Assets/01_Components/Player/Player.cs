using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class EventManager
{

    public static event System.Action OnPlayerMove;
    public static void Fire_OnPlayerMove() { OnPlayerMove?.Invoke(); }
}

public class Player : MonoBehaviour
{
    protected Transform mT;
    protected Animator mAnim;

    protected float limitY = 10;

    protected SkinnedMeshRenderer mesh;

    protected int speed = 10;

    public List<ParticleSystem> bubbleFx;

    public static bool isActiveGame;
    public static bool upMovement;
    public static bool isSwim;


    protected virtual void Awake()
    {
        mT = transform;
        mAnim = GetComponentInChildren<Animator>();

        isSwim = true;

        mesh = GetComponentInChildren<SkinnedMeshRenderer>();

        BubbleFxActive(false);

    }

    protected void Update()
    {

        StartMovement();
    }

    protected virtual void StartMovement()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mT.position += Vector3.down/4;
            upMovement = false;

            BubbleFxActive(true);

            if (isSwim)
            {

                ResetAnim(AnimParam.idle);
                mAnim.SetTrigger(AnimParam.swim);
            }
            

            isSwim = false;

        }
        else if (Input.GetMouseButton(0))
        {

            mT.position += (Vector3.forward + Vector3.down / 4) * Time.deltaTime * speed;
            mT.position = new Vector3(0, Mathf.Clamp(mT.position.y, -limitY, limitY), mT.position.z);

        }
        else if (Input.GetMouseButtonUp(0))
        {
            upMovement = true;


        }

        if (upMovement)
        {
            mT.position += (Vector3.forward + Vector3.up/2) * Time.deltaTime * speed;
        }
    }

    protected void ResetAnim(string anim)
    {
        mAnim.ResetTrigger(anim);
    }
    
    protected void BubbleFxActive(bool state)
    {
        foreach (var item in bubbleFx)
        {
            item.gameObject.SetActive(state);
        }
    }
  

}
