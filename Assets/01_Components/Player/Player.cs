using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public partial class EventManager
{

    public static event System.Action OnPlayerMove;
    public static void Fire_OnPlayerMove() { OnPlayerMove?.Invoke(); }
}

public enum DamagePhase
{
    None,
    Low,
    Medium,
    Hard,
    Death,
    Default

}


public class Player : MonoBehaviour
{

    protected Transform mT;
    protected Animator mAnim;

    protected SkinnedMeshRenderer mesh;

    public static float breath { get; protected set; }

    protected float limitY = 10;

    protected int speed = 10;

    public static bool isActiveGame;
    public static bool upMovement;
    public static bool breathing;
    public static bool isSurface;

    public List<ParticleSystem> bubbleFx;

    protected virtual void Awake()
    {
        Application.targetFrameRate = 30;
        mT = transform;
        mAnim = GetComponentInChildren<Animator>();

        breath = 100;

        isActiveGame = true;

        isSurface = true;

        mesh = GetComponentInChildren<SkinnedMeshRenderer>();

    }

    protected void BubbleFxActive(bool state)
    {
        foreach (var item in bubbleFx)
        {
            item.gameObject.SetActive(state);
        }
    }

    protected DamagePhase PhaseEnum()
    {

        if (breath >= 75)
            return DamagePhase.None;

        if (breath < 75 && breath >= 50)
            return DamagePhase.Low;

        else if (breath < 50 && breath >= 25)
            return DamagePhase.Medium;

        else if (breath < 25 && breath >= 10)
            return DamagePhase.Hard;

        else if (breath < 5)
            return DamagePhase.Death;

        else
            return DamagePhase.Default;

    }

    void RedToWhite(int index)
    {
        if (mesh.materials[index].color == Color.red) mesh.materials[index].DOColor(Color.white, 1);
    }


    void WhiteToRed(int index)
    {
        if (mesh.materials[index].color == Color.white) mesh.materials[index].DOColor(Color.red, 1);
    }

    protected void BreathControl()
    {

        PhaseEnum();


        switch (PhaseEnum())
        {
            case DamagePhase.None:

                RedToWhite(0);

                break;

            case DamagePhase.Low:

                WhiteToRed(0);
                RedToWhite(1);

                break;

            case DamagePhase.Medium:

                WhiteToRed(1);
                RedToWhite(2);

                break;

            case DamagePhase.Hard:

                WhiteToRed(2);

                break;

            case DamagePhase.Death:

                isActiveGame = false;
                mAnim.SetTrigger(AnimParam.die);

                break;

            default:

                break;
        }

    }



    

    protected void ResetAnim(string anim)
    {
        mAnim.ResetTrigger(anim);
    }
    
    


    protected void DecreaseBreath()
    {
        if (breath > 0)
        {
            breath -= 12 * Time.deltaTime;
        }

    }

    protected void IncreaseBreath()
    {
        if (breath<100)
        {
            breath += 8*Time.deltaTime;
            
        }

    }

}
