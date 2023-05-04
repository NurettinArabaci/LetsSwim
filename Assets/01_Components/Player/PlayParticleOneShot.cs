using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayParticleOneShot : MonoBehaviour
{
    [SerializeField] VfxType vfxType;

    ParticleSystem[] particles;

    private void Awake()
    {
        particles = GetComponentsInChildren<ParticleSystem>();

        EventManager.OnPlayVfxOneShot += PlayParticles;
    }
   

    public void PlayParticles(VfxType vfxType)
    {
        if (vfxType != this.vfxType)
            return;

        foreach (var item in particles)
        {
            item.Play();
        }
    }

    private void OnDisable()
    {
        EventManager.OnPlayVfxOneShot -= PlayParticles;
    }
}