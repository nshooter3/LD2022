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
        HideAll();
        fireParticles.gameObject.SetActive(true);
    }

    public void SetWater()
    {
        HideAll();
        waterParticles.gameObject.SetActive(true);
    }

    public void SetGrass()
    {
        HideAll();
        grassParticles.gameObject.SetActive(true);
    }

    public void HideAll()
    {
        fireParticles.gameObject.SetActive(false);
        waterParticles.gameObject.SetActive(false);
        grassParticles.gameObject.SetActive(false);
    }
}
