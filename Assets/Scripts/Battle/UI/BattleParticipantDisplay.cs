using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BattleParticipantDisplay : MonoBehaviour
{
    [SerializeField]
    private Image healthBar;
    [SerializeField]
    private Button targetButton;
    [SerializeField]
    private Image intentSprite;
    [SerializeField]
    private Animator anims;
    public int maxHp { private get; set; }

    public void SetHealth(int health)
    {
        SetBarValue(healthBar, health, maxHp);
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
            default:
                break;
        }
    }
}
