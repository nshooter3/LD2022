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
    private TextMeshProUGUI intentText;
    public int maxHp { private get; set; }

    public StatusDisplay statusPanel;

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
        intentText.text = intent;
    }

    public void DisplayStatus()
    {
        statusPanel.DisplayStatus();
    }
}
