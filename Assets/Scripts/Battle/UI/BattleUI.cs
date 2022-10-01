using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BattleUI : MonoBehaviour
{
    public static BattleUI instance { get; private set; }

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
    private List<GameObject> areaOfEffectSelectionIndicators;
    private bool useAreaOfEffectIndicators;

    [SerializeField]
    private MoveTimer moveTimer;

    private int chosenAction;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (!useAreaOfEffectIndicators)
        {
            if (EventSystem.current.currentSelectedGameObject == null)
            {
                selectionIndicator.SetActive(false);
            }
            else
            {
                PositionSelectionIndicator(EventSystem.current.currentSelectedGameObject, selectionIndicator);
            }
        }
    }

    public void Initialize(BattlePlayer player, List<BattleParticipant> enemies)
    {
        this.player = player;
        this.enemies = enemies;

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
        UpdateHealth();

        foreach (Button button in actionButtons)
        {
            button.gameObject.SetActive(false);
        }
        HideSelectionIndicators();
    }

    public void UpdateHealth()
    {
        playerDisplay.SetHealth(player.currentHp);
        for (int i = 0; i < enemies.Count; i++)
        {
            enemyDisplays[i].SetHealth(enemies[i].currentHp);
        }
    }

    public void UpdateIntents()
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            if (!enemies[i].Dead)
            {
                enemyDisplays[i].SetIntent(enemies[i].currentAction.GetIntentDisplay());
            }
        }
    }

    public void PromptAction(List<BattleAction> actions)
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

        if (actions[chosenAction].AreaOfEffect)
        {
            useAreaOfEffectIndicators = true;
            for (int i = 0; i < enemies.Count; i++)
            {
                if (!enemies[i].Dead)
                {
                    PositionSelectionIndicator(enemyDisplays[i].gameObject, areaOfEffectSelectionIndicators[i]);
                }
            }
        }
    }

    public void ChooseTarget(int targetIndex)
    {
        HideSelectionIndicators();
        enemyDisplays.ForEach(display => display.SetTargetButtonActive(false));

        List<BattleParticipant> targetEnemies = GetTargetEnemiesList(targetIndex);
        player.ChoosePlayerAction(actions[chosenAction], enemies);
    }

    private void PositionSelectionIndicator(GameObject targetObject, GameObject currentSelectionIndicator)
    {
        currentSelectionIndicator.transform.position = targetObject.transform.position + Vector3.left * 60;
        currentSelectionIndicator.SetActive(true);
    }

    private void ChooseRandomAction()
    {
        HideSelectionIndicators();
        List<BattleParticipant> targetEnemies = GetTargetEnemiesList(Random.Range(0, enemies.Count - 1));
        player.ChoosePlayerAction(actions[Random.Range(0, actions.Count - 1)], targetEnemies);
    }

    private List<BattleParticipant> GetTargetEnemiesList(int targetIndex)
    {
        List<BattleParticipant> targetEnemies = new List<BattleParticipant>();
        if (actions[chosenAction].AreaOfEffect)
        {
            targetEnemies = enemies;
        }
        else
        {
            targetEnemies.Add(enemies[targetIndex]);
        }
        return targetEnemies;
    }

    private void HideSelectionIndicators()
    {
        EventSystem.current.SetSelectedGameObject(null);
        areaOfEffectSelectionIndicators.ForEach(indicator => indicator.SetActive(false));
        useAreaOfEffectIndicators = false;
    }
}
