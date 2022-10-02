using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusDisplay : MonoBehaviour
{
    [SerializeField]
    private BattleParticipant battleParticipant;

    [SerializeField]
    private List<Image> statusIcons = new List<Image>();

    public void DisplayStatus()
    {
        foreach (var statusIcon in statusIcons)
        {
            statusIcon.gameObject.SetActive(false);
        }

        int statusIconIndex = 0;
        foreach (var status in battleParticipant.statuses)
        {
            statusIcons[statusIconIndex].gameObject.SetActive(true);
            statusIcons[statusIconIndex].sprite = status.StatusSprite;
            ++statusIconIndex;
        }
    }
}
