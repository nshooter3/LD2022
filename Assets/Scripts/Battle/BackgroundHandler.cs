using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundHandler : MonoBehaviour
{

    public Material material;
    Material cloneMaterial;
    public Texture[] mainTextures;
    public Texture[] distortionTextures;
    //public Color color;

    // Start is called before the first frame update
    void Start()
    {
        cloneMaterial = new Material(material);
        RandomizeBG();
    }

    Color RandomColor()
    {
        return new Color(Random.Range(0f, .7f), Random.Range(0f, .7f), Random.Range(0f, .7f));

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
        cloneMaterial.SetTexture("_MainTex", mainTextures[Random.Range(0, mainTextures.Length)]); //Set main texture
        cloneMaterial.SetTexture("_SecondTex", mainTextures[Random.Range(0, mainTextures.Length)]); //Set secondary texture
        cloneMaterial.SetTexture("_DistortionNormal", distortionTextures[Random.Range(0, distortionTextures.Length - 1)]); //Set distortion texture
        cloneMaterial.SetColor("_Color", RandomColor());
        cloneMaterial.SetColor("_Color2", RandomColor());

        RenderSettings.skybox = cloneMaterial;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
