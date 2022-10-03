using UnityEngine;

public class ScaleToUI : MonoBehaviour
{
    public GameObject anchor;
    private Vector3 originalScale;

    private void Start()
    {
        originalScale = transform.localScale;
    }

    private void Update()
    {
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(anchor.transform.position);
        worldPos.z = 0;
        transform.position = worldPos;
        transform.localScale = originalScale * BattleUI.instance.CanvasScale;
    }
}
