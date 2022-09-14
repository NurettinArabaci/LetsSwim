using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class EventManager
{

    public static event System.Action OnPlayerMove;
    public static void Fire_OnPlayerMove() { OnPlayerMove?.Invoke(); }
}

public enum DamagePhaseType
{
    None,
    Low,
    Medium,
    Hard,
    Die,
    Default

}


public class Player : MonoBehaviour
{

    protected Transform mT;
    protected Animator mAnim;
    protected Rigidbody rb;

    protected SkinnedMeshRenderer mesh;

    public static float breath { get; protected set; }

    public static float colorChangeAmount;

    protected float limitY = 10;

    protected int speed = 10;

    public static bool isActiveGame;
    public static bool upMovement;
    public static bool breathing;
    protected bool isDeath;

    public List<ParticleSystem> bubbleFx;

    protected virtual void Awake()
    {
        Application.targetFrameRate = 30;

        mT = transform;

        colorChangeAmount = 2;

        mAnim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody>();
        mesh = GetComponentInChildren<SkinnedMeshRenderer>();

        breath = 100;

        isActiveGame = false;



    }

    protected void BubbleFxActive(bool state)
    {
        foreach (var item in bubbleFx)
        {
            item.gameObject.SetActive(state);
        }
    }

    protected DamagePhaseType DamagePhase()
    {

        if (breath >= 75)
            return DamagePhaseType.None;

        if (breath < 75 && breath >= 50)
            return DamagePhaseType.Low;

        else if (breath < 50 && breath >= 25)
            return DamagePhaseType.Medium;

        else if (breath < 25 && breath >= 10)
            return DamagePhaseType.Hard;

        else if (breath <= 0)
            return DamagePhaseType.Die;

        else
            return DamagePhaseType.Default;

    }

    void RedToWhite(int index)
    {
        //        if (mesh.materials[index].color == Color.red) mesh.materials[index].DOColor(Color.white, 1);
    }


    void WhiteToRed(int index)
    {
        //      if (mesh.materials[index].color == Color.white) mesh.materials[index].DOColor(Color.red, 1);
    }

    protected void BreathControl()
    {

        switch (DamagePhase())
        {
            case DamagePhaseType.None:

                RedToWhite(0);

                break;

            case DamagePhaseType.Low:

                WhiteToRed(0);
                RedToWhite(1);

                break;

            case DamagePhaseType.Medium:

                WhiteToRed(1);
                RedToWhite(2);

                break;

            case DamagePhaseType.Hard:

                WhiteToRed(2);

                break;

            case DamagePhaseType.Die:

                Die();

                break;

            default:

                break;
        }

    }


    protected void AnimationChanging(string endAnim, string startAnim)
    {
        if (mAnim.GetCurrentAnimatorStateInfo(0).IsName(endAnim))
        {
            mAnim.ResetTrigger(endAnim);
            mAnim.SetTrigger(startAnim);
        }
    }



    public static float Stamina
    {
        get { return PlayerPrefs.GetFloat("Stamina", 15); }
        set { PlayerPrefs.SetFloat("Stamina", value); }
    }

    public static int Distance
    {
        get { return PlayerPrefs.GetInt("Distance", 0); }
        set { PlayerPrefs.SetInt("Distance", value); }
    }

    public static float CoinIncrease
    {
        get { return PlayerPrefs.GetFloat("CoinIncrease", 0.5f); }
        set { PlayerPrefs.SetFloat("CoinIncrease", value); }
    }


    public static float Coin
    {
        get { return PlayerPrefs.GetFloat("Coin", 0); }
        set { PlayerPrefs.SetFloat("Coin",value); }
    }

    public static float CoinTemp
    {
        get { return PlayerPrefs.GetFloat("CoinTemp", 0); }
        set { PlayerPrefs.SetFloat("CoinTemp", value); }
    }

    protected void DecreaseBreath()
    {
        if (breath > 0)
        {
            breath -= Stamina * Time.deltaTime;
            colorChangeAmount -= Stamina * Time.deltaTime / 30;
            ColorLevel();
        }

    }

    protected void IncreaseBreath()
    {
        if (breath < 90)
        {
            breath += Stamina * 0.5f * Time.deltaTime;

            ColorLevel();
        }

    }




    void ColorLevel()
    {
        if (breath < 90)
        {
            mesh.materials[0].color = new Color(1, breath / 90, breath / 90);
        }

    }

    protected void Die()
    {
        isActiveGame = false;
        mAnim.SetTrigger(AnimParam.die);
        rb.velocity = Vector3.zero;

        StartCoroutine(RestartButtonCR());

        Coin += CoinTemp;
        CoinTemp = 0;

        isDeath = true;

        CamManager.instance.OnFinishGame();

    }

    IEnumerator RestartButtonCR()
    {
        yield return new WaitForSeconds(2);
        UIManager.instance.OpenRestartPanel();
    }
}
