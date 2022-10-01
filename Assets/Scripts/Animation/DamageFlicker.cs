using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Flickers all sprites on the entity after taking damage.
/// </summary>
public class DamageFlicker : MonoBehaviour
{
    public Color col1 = Color.red;
    public Color col2 = Color.white;
    public float colStrength = 0.7f;
    private const float FLICKER_TIME = 0.5f;
    private const float INVULNERABILITY_PULSE_TIME = 0.32f;
    private const int INVULNERABILITY_PULSE_COUNT = 2;
    private const float INVULNERABILITY_PULSE_STRENGTH = 0.3f;
    private const float COL1_TIME_RATIO = 0.25f;
    private List<SpriteMult> sprites = new List<SpriteMult>();
    private Color curCol;

    private class SpriteMult
    {
        private Material material;

        public SpriteMult(SpriteRenderer sprite)
        {
            material = new Material(sprite.sharedMaterial);
            sprite.sharedMaterial = this.material;
        }

        public void SetMultStrength(float strength, Color col)
        {
            material.SetColor("_MultColor", col);
            material.SetFloat("_MultStrength", strength);
        }

        public void DestroySelf()
        {
            Destroy(material);
            material = null;
        }
    }

    public void StartFlicker(bool doInvulnerabilityPulse = false)
    {
        StopAllCoroutines();
        StartCoroutine(Flicker(doInvulnerabilityPulse));
    }

    private void Start()
    {
        foreach (SpriteRenderer sprite in GetComponentsInChildren<SpriteRenderer>(true))
        {
            sprites.Add(new SpriteMult(sprite));
        }
    }

    private void OnDestroy()
    {
        sprites.ForEach(p => p.DestroySelf());
    }

    private IEnumerator Flicker(bool doInvulnerabilityPulse)
    {
        curCol = col1;
        float counter = 0f;
        //Flicker from red to white when hit to give feedback for taking damage.
        while (counter < FLICKER_TIME)
        {
            counter += Time.deltaTime;
            if ((counter / FLICKER_TIME) > COL1_TIME_RATIO)
            {
                curCol = col2;
            }
            sprites.ForEach(p => p.SetMultStrength((1 - (counter / FLICKER_TIME)) * colStrength, curCol));
            yield return null;
        }

        //Pulse color after damage flicker. Used to show invulnerability period on player.
        if (doInvulnerabilityPulse)
        {
            int curPulse = 1;
            while (curPulse <= INVULNERABILITY_PULSE_COUNT)
            {
                float multStrength = 0f;
                counter = 0f;
                while (counter < INVULNERABILITY_PULSE_TIME)
                {
                    counter += Time.deltaTime;
                    //Interpolate the strength from 0, to 1, and back to 0 over time for pulse effect.
                    if (counter < INVULNERABILITY_PULSE_TIME / 2f)
                    {
                        multStrength = (counter / INVULNERABILITY_PULSE_TIME) * 2f;
                    }
                    else
                    {
                        multStrength = (1f - (counter / INVULNERABILITY_PULSE_TIME)) * 2f;
                    }
                    sprites.ForEach(p => p.SetMultStrength((multStrength) * INVULNERABILITY_PULSE_STRENGTH, curCol));
                }
                yield return null;
                curPulse++;
            }
        }
    }
}
