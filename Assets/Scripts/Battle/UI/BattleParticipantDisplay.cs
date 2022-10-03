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
    private TextMeshProUGUI intentTextMesh;
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

    public void SetIntent(IntentType intent, string intentText)
    {
        anims.ResetTrigger("NoIntent");
        intentSprite.color = Color.white;
        switch (intent)
        {
            case IntentType.ATTACK:
                anims.SetTrigger("Attacking");
                break;
            case IntentType.BUFF:
                anims.SetTrigger("Buffing");
                break;
            case IntentType.DEBUFF:
                anims.SetTrigger("Debuffing");
                break;
            case IntentType.NONE:
                intentSprite.color = Color.clear;
                anims.SetTrigger("NoIntent");
                break;
        }
        intentTextMesh.text = intentText;
    }

    public void DisplayStatus(List<Status> activeStatuses)
    {
        statusPanel.DisplayStatus(activeStatuses);
    }
}
