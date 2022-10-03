using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusDisplay : MonoBehaviour
{
    [SerializeField]
    private List<Image> statusIcons = new List<Image>();
    private const float ICON_ANIMATION_RATE = 0.1f;

    private List<StatusSpriteCounter> spriteCounters = new List<StatusSpriteCounter>();

    class StatusSpriteCounter
    {
        public Type statusType;
        public List<Sprite> sprites;
        public float spriteCounter;
        public Image statusIcon;

        public StatusSpriteCounter()
        {
        }

        public StatusSpriteCounter(Type statusType, List<Sprite> sprites, int spriteCounter, Image statusIcon)
        {
            this.statusType = statusType;
            this.sprites = sprites;
            this.spriteCounter = spriteCounter;
            this.statusIcon = statusIcon;
        }
    }

    private void Start()
    {
        foreach (var statusIcon in statusIcons)
        {
            spriteCounters.Add(new StatusSpriteCounter());
        }
    }

    public void DisplayStatus(List<Status> activeStatuses)
    {
        foreach (var statusIcon in statusIcons)
        {
            statusIcon.gameObject.SetActive(false);
        }

        for (int i = 0; i < activeStatuses.Count; i++)
        {
            if (i < activeStatuses.Count)
            {
                Status status = activeStatuses[i];
                Image statusIcon = statusIcons[i];
                statusIcon.gameObject.SetActive(true);
                if (spriteCounters[i].statusType != activeStatuses[i].GetType())
                {
                    statusIcon.sprite = status.statusSprites[0];
                    spriteCounters[i] = new StatusSpriteCounter(status.GetType(), status.statusSprites, 0, statusIcon);
                }
            }
            else
            {
                spriteCounters[i] = new StatusSpriteCounter();
            }
        }
    }

    public void Update()
    {
        for (int i = 0; i < spriteCounters.Count; i++)
        {
            StatusSpriteCounter spriteCounter = spriteCounters[i];
            if (spriteCounter.statusType == null)
            {
                continue;
            }
            spriteCounter.statusIcon.sprite = spriteCounter.sprites[(int)(spriteCounter.spriteCounter / ICON_ANIMATION_RATE) % spriteCounter.sprites.Count];
            spriteCounter.spriteCounter += Time.deltaTime;
        }
    }
}
