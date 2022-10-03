using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundHandler : MonoBehaviour
{
    
    public SpriteRenderer renderer;
    [SerializeField]
    //Material cloneMaterial;
    public Sprite[] mainTextures;

    // Start is called before the first frame update
    void Start()
    {
        //cloneMaterial = new Material(GetComponent<Renderer>().material);
        renderer.sprite = mainTextures[Random.Range(0, mainTextures.Length - 1)];
        //GetComponent<Renderer>().material = cloneMaterial;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
