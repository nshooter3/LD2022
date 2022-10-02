using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusDisplay : MonoBehaviour
{
    [SerializeField]
    private List<Image> statusIcons = new List<Image>();

    public void DisplayStatus(List<Status> activeStatuses)
    {
        foreach (var statusIcon in statusIcons)
        {
            statusIcon.gameObject.SetActive(false);
        }

        int statusIconIndex = 0;
        foreach (var status in activeStatuses)
        {
            statusIcons[statusIconIndex].gameObject.SetActive(true);
            statusIcons[statusIconIndex].sprite = status.statusSprite;
            ++statusIconIndex;
        }
    }
}
