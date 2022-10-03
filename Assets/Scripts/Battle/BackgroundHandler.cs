using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundHandler : MonoBehaviour
{

    public Material material;
    public Texture[] mainTextures;
    public Texture[] distortionTextures;
    //public Color color;

    // Start is called before the first frame update
    void Start()
    {
        RandomizeBG();
    }

    Color RandomColor()
    {
        return new Color(Random.Range(0f, 1.0f), Random.Range(0f, 1.0f), Random.Range(0f, 1.0f));

    }

    /*

    void Start()
    {
        Renderer rend = GetComponent<Renderer> ();

        rend.material = new Material(shader);
        rend.material.mainTexture = texture;
        rend.material.color = color;
    }
     */

    public void RandomizeBG()
    {
        material.SetTexture("_MainTex", mainTextures[Random.Range(0, mainTextures.Length - 1)]); //Set main texture
        material.SetTexture("_SecondTex", mainTextures[Random.Range(0, mainTextures.Length - 1)]); //Set secondary texture
        material.SetTexture("_DistortionNormal", distortionTextures[Random.Range(0, distortionTextures.Length - 1)]); //Set distortion texture
        material.SetColor("_Color", RandomColor());
        material.SetColor("_Color2", RandomColor());

        RenderSettings.skybox = material;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
