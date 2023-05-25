using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatRestRoad : MonoBehaviour
{
    ParticleSystem particle;
    MeshRenderer mesh;

    private void Awake()
    {
        mesh = GetComponent<MeshRenderer>();
        mesh.enabled = true;
        particle = GetComponentInChildren<ParticleSystem>();
    }

    public async void EatRoad()
    {
        await System.Threading.Tasks.Task.Delay(300);
        particle.Play();
        mesh.enabled = false;
        Destroy(gameObject, 0.5f);
        
    }
}
