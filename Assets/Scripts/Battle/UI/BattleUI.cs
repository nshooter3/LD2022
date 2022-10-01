using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BattleUI : MonoBehaviour
{
    public static BattleUI instance { get; private set; }

    [SerializeField]
    private BattlePlayer player;
    [SerializeField]
    private Button button;
    [SerializeField]
    private TextMeshProUGUI playerHealth;
    [SerializeField]
    private TextMeshProUGUI enemyHealth;
    private List<BattleAction> actions;
    private List<BattleParticipant> targets;

    private BattleParticipant playerParticipant;
    private List<BattleParticipant> enemies;

    private void Awake()
    {
        instance = this;
    }

    public void Initialize(BattleParticipant player, List<BattleParticipant> enemies)
    {
        this.playerParticipant = player;
        this.enemies = enemies;
        UpdateHealth();
    }

    public void UpdateHealth()
    {
        playerHealth.text = player.currentHp.ToString();
        enemyHealth.text = enemies[0].currentHp.ToString();
    }

    public void PromptAction(List<BattleAction> actions, List<BattleParticipant> targets)
    {
        this.actions = actions;
        this.targets = targets;
        button.enabled = true;
    }

    public void ChooseAction()
    {
        button.enabled = false;
        player.ChoosePlayerAction(actions[0], targets);
    }
}
