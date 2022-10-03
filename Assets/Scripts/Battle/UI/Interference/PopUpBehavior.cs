using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopUpBehavior : MonoBehaviour
{
    public bool isPoppingIn;
    public float popInDuration;
    private float currentTime;
    private float randomScaleModifier;

    [SerializeField]
    private List<Image> sprites;

    // Start is called before the first frame update
    void Start()
    {
        sprites.ForEach(p => p.enabled = false);
        sprites[Random.Range(0, sprites.Count)].enabled = true;
        transform.localScale = Vector3.zero;
        isPoppingIn = true;
        currentTime = 0;
        randomScaleModifier = Random.Range(2f, 3f);
    }

    // Update is called once per frame
    void Update()
    {
        if (isPoppingIn)
        {
           transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one * randomScaleModifier, currentTime / popInDuration);
        }
        if (transform.localScale.x >= randomScaleModifier)
        {
            isPoppingIn = false;
        }
        if (currentTime <= 1 * popInDuration)
        {
            currentTime += 0.1f;
        }
    }
}
