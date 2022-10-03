using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBossParticles : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem fireParticles;

    [SerializeField]
    private ParticleSystem waterParticles;

    [SerializeField]
    private ParticleSystem grassParticles;

    public void SetFire()
    {

    }

    public void HideAll()
    {
        fireParticles.gameObject.SetActive(false);
        waterParticles.gameObject.SetActive(false);
        grassParticles.gameObject.SetActive(false);
    }
}
