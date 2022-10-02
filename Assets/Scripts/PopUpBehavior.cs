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
        isPoppingIn = true;
        currentTime = 0;
        randomScaleModifier = Random.Range(1f, 3f);
    }

    // Update is called once per frame
    void Update()
    {
        if (isPoppingIn)
        {
            GetComponent<Transform>().localScale = Vector3.Lerp(Vector3.zero, Vector3.one* randomScaleModifier, currentTime/ popInDuration);
        }
        if (GetComponent<Transform>().localScale == Vector3.one*randomScaleModifier)
        {
            isPoppingIn = false;
        }
        if (currentTime <= 1.0* popInDuration)
        {
            currentTime += 0.1f;
        }
    }
}
