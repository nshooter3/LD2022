using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BattleUI : MonoBehaviour
{
    public static BattleUI instance { get; private set; }

    [SerializeField]
    private BattlePlayer player;
    private List<BattleAction> actions;
    private List<BattleParticipant> enemies;

    [SerializeField]
    private PlayerDisplay playerDisplay;
    [SerializeField]
    private List<BattleParticipantDisplay> enemyDisplays;

    [SerializeField]
    private List<Button> actionButtons;

    private int chosenAction;

    private void Awake()
    {
        instance = this;
    }

    public void Initialize(BattlePlayer player, List<BattleParticipant> enemies)
    {
        playerDisplay.maxHp = player.MaxHp;
        playerDisplay.maxMp = player.MaxMp;
        for (int i = 0; i < enemyDisplays.Count; i++)
        {
            BattleParticipantDisplay enemyDisplay = enemyDisplays[i];
            if (i < enemies.Count)
            {
                enemyDisplay.maxHp = enemies[i].MaxHp;
                enemyDisplay.gameObject.SetActive(true);
            }
            else
            {
                enemyDisplay.gameObject.SetActive(false);
            }
        }
        UpdateHealth(player, enemies);

        foreach (Button button in actionButtons)
        {
            button.gameObject.SetActive(false);
        }
    }

    public void UpdateHealth(BattlePlayer player, List<BattleParticipant> enemies)
    {
        playerDisplay.SetHealth(player.currentHp);
        for (int i = 0; i < enemies.Count; i++)
        {
            enemyDisplays[i].SetHealth(enemies[i].currentHp);
        }
    }

    public void PromptAction(List<BattleAction> actions, List<BattleParticipant> enemies)
    {
        for (int i = 0; i < actionButtons.Count; i++)
        {
            Button button = actionButtons[i];
            if (i < actions.Count)
            {
                button.gameObject.SetActive(true);
                button.GetComponentInChildren<TextMeshProUGUI>().text = actions[i].ActionName;
            }
        }
        this.actions = actions;
        this.enemies = enemies;
    }

    public void ChooseAction(int actionIndex)
    {
        chosenAction = actionIndex;

        foreach (Button button in actionButtons)
        {
            button.gameObject.SetActive(false);
        }

        for (int i = 0; i < enemies.Count; i++)
        {
            if (!enemies[i].Dead)
            {
                enemyDisplays[i].SetTargetButtonActive(true);
            }
        }
    }

    public void ChooseTarget(int targetIndex)
    {
        List<BattleParticipant> targetEnemies = new List<BattleParticipant>();
        if (targetIndex >= 0)
        {
            targetEnemies.Add(enemies[targetIndex]);
        }
        else
        {
            targetEnemies = enemies;
        }
        player.ChoosePlayerAction(actions[chosenAction], enemies);
    }
}
