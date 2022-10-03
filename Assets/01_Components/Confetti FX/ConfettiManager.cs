using System.Collections;
using UnityEngine;

public partial class EventManager
{

    public static event System.Action<float> OnConfettiPlay;
    public static void Fire_OnConfettiPlay(float _wait) { OnConfettiPlay?.Invoke(_wait); }

}
public class ConfettiManager : MonoBehaviour
{
    ParticleSystem _particleSystem;
    private void Awake()
    {
        _particleSystem = GetComponent<ParticleSystem>();

        EventManager.OnConfettiPlay += OnConfettiPlay;
    }

    void OnConfettiPlay(float wait)
    {
        StartCoroutine(OnConfettiPlayCR(wait));
    }
    IEnumerator OnConfettiPlayCR(float _wait)
    {
        yield return new WaitForSeconds(_wait);
        _particleSystem.Play();
    }

    private void OnDisable()
    {
        EventManager.OnConfettiPlay -= OnConfettiPlay;
    }
}
