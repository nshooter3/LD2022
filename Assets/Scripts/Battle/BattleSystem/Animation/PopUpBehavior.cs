using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpBehavior : MonoBehaviour
{
    public bool isPoppingIn;
    public float popInDuration;
    private float currentTime;
    private float randomScaleModifier;

    // Start is called before the first frame update
    void Start()
    {
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
