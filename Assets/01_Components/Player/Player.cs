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
    protected Rigidbody rb;

    [SerializeField] protected Animator lowHpAnim;

    protected SkinnedMeshRenderer mesh;

    public float breath;

    protected float limitY = 10;

    protected int speed = 10;

    public static bool isActiveGame;
    public static bool upMovement;
    public static bool breathing;
    protected bool isFinish = false;

    protected bool isDeath()
    {
        if (breath <= 0)
        { 
            Die();
            isFinish = true;
            return true;
        }

        return false;
    }

    public List<ParticleSystem> bubbleFx;

    protected virtual void Awake()
    {
        Application.targetFrameRate = 30;

        CoinTemp = 0;

        mT = transform;

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
        get { return PlayerPrefs.GetFloat("Stamina", 25); }
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


    public static float Wallet
    {
        get { return PlayerPrefs.GetFloat("Wallet", 0); }
        set { PlayerPrefs.SetFloat("Wallet", value); }
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

        Wallet += CoinTemp;
        CoinTemp = 0;

        lowHpAnim.SetFloat("LowHpValue", 1);

        CamManager.instance.OnFinishGame();

        ScoreManager.ScoreUpdate();

    }

    IEnumerator RestartButtonCR()
    {
        yield return new WaitForSeconds(2.3f);
        UIManager.instance.OpenRestartPanel();
        
    }

    
}
