using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BattleParticipantDisplay : MonoBehaviour
{
    [SerializeField]
    private Image healthBar;
    [SerializeField]
    private Button targetButton;
    public Button TargetButton { get { return targetButton; } }
    [SerializeField]
    private Image intentSprite;
    [SerializeField]
    private Animator anims;
    public int maxHp { private get; set; }
    [SerializeField]
    private TextMeshProUGUI healthNumbers;

    public StatusDisplay statusPanel;

    public void SetHealth(int health)
    {
        SetBarValue(healthBar, health, maxHp);
        healthNumbers.text = health + "/" + maxHp;
    }

    protected static void SetBarValue(Image bar, int value, int maxValue)
    {
        bar.fillAmount = value / (float)maxValue;
    }

    public void SetTargetButtonActive(bool active)
    {
        targetButton.interactable = active;
    }

    public void SetIntent(string intent)
    {
        anims.ResetTrigger("NoIntent");
        intentSprite.color = Color.white;
        switch (intent)
        {
            case "Attack":
                anims.SetTrigger("Attacking");
                break;
            case "Buff":
                anims.SetTrigger("Buffing");
                break;
            case "Debuff":
                anims.SetTrigger("Debuffing");
                break;
            case "NoIntent":
                intentSprite.color = Color.clear;
                anims.SetTrigger("NoIntent");
                break;
            default:
                break;
        }
    }

    public void DisplayStatus(List<Status> activeStatuses)
    {
        statusPanel.DisplayStatus(activeStatuses);
    }
}
