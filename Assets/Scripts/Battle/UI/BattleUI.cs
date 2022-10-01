using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
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

    [SerializeField]
    private GameObject selectionIndicator;

    [SerializeField]
    private MoveTimer moveTimer;

    private int chosenAction;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (EventSystem.current.currentSelectedGameObject == null)
        {
            selectionIndicator.SetActive(false);
        }
        else
        {
            selectionIndicator.transform.position = EventSystem.current.currentSelectedGameObject.transform.position + Vector3.left * 60;
            selectionIndicator.SetActive(true);
        }
    }

    public void Initialize(BattlePlayer player, List<BattleParticipant> enemies)
    {
        playerDisplay.maxHp = player.MaxHp;
        playerDisplay.maxMp = player.MaxMp;
        for (int i = 0; i < enemyDisplays.Count; i++)
        {
            BattleParticipantDisplay enemyDisplay = enemyDisplays[i];
            enemyDisplay.SetTargetButtonActive(false);
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
        Button firstSelectableAction = null;
        for (int i = 0; i < actionButtons.Count; i++)
        {
            Button button = actionButtons[i];
            if (i < actions.Count)
            {
                button.gameObject.SetActive(true);
                button.GetComponentInChildren<TextMeshProUGUI>().text = actions[i].ActionName;
            }
            if (firstSelectableAction == null)
            {
                firstSelectableAction = button;
            }
        }
        this.actions = actions;
        this.enemies = enemies;

        EventSystem.current.SetSelectedGameObject(firstSelectableAction.gameObject);

        moveTimer.StartTimer(ChooseRandomAction);
    }

    public void ChooseAction(int actionIndex)
    {
        chosenAction = actionIndex;

        foreach (Button button in actionButtons)
        {
            button.gameObject.SetActive(false);
        }

        BattleParticipantDisplay firstSelectableEnemyDisplay = null;
        for (int i = 0; i < enemies.Count; i++)
        {
            if (!enemies[i].Dead)
            {
                enemyDisplays[i].SetTargetButtonActive(true);
                if (firstSelectableEnemyDisplay == null)
                {
                    firstSelectableEnemyDisplay = enemyDisplays[i];
                }
            }
        }

        EventSystem.current.SetSelectedGameObject(firstSelectableEnemyDisplay.gameObject);
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

        EventSystem.current.SetSelectedGameObject(null);

        enemyDisplays.ForEach(display => display.SetTargetButtonActive(false));

        player.ChoosePlayerAction(actions[chosenAction], enemies);
    }

    private void ChooseRandomAction()
    {
        List<BattleParticipant> targetEnemies = new List<BattleParticipant>();
        targetEnemies.Add(enemies[Random.Range(0, enemies.Count - 1)]);
        player.ChoosePlayerAction(actions[Random.Range(0, actions.Count - 1)], targetEnemies);
    }
}
