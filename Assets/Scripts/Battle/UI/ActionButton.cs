using UnityEngine;

public class ActionButton : MonoBehaviour
{
    public int index;

    float yScale;

    private void Start()
    {
        yScale = transform.localScale.y;
    }

    public void SetInverted(bool inverted)
    {
        if (inverted)
        {
            transform.localScale = new Vector3(transform.localScale.x, -yScale, transform.localScale.z);
        }
        else
        {
            transform.localScale = new Vector3(transform.localScale.x, yScale, transform.localScale.z);
        }
    }
}
