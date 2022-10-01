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
        playerDisplay.SetTargetButtonActive(false);
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
        UpdateStatBars();

        foreach (Button button in actionButtons)
        {
            button.gameObject.SetActive(false);
        }
        HideSelectionIndicators();
    }

    public void UpdateStatBars()
    {
        playerDisplay.SetHealth(player.currentHp);
        playerDisplay.SetMp(player.currentMp);
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
                BattleAction action = actions[i];
                button.gameObject.SetActive(true);
                button.GetComponentInChildren<TextMeshProUGUI>().text = action.ActionName;

                if (player.CanUseAction(action))
                {
                    button.interactable = true;
                    if (firstSelectableAction == null)
                    {
                        firstSelectableAction = button;
                    }
                }
                else
                {
                    button.interactable = false;
                }
            }
        }
        this.actions = actions;

        EventSystem.current.SetSelectedGameObject(firstSelectableAction.gameObject);

        moveTimer.StartTimer(ChooseRandomAction);
    }

    public void ChooseAction(int actionIndex)
    {
        chosenAction = actionIndex;
        BattleAction action = actions[chosenAction];

        foreach (Button button in actionButtons)
        {
            button.gameObject.SetActive(false);
        }

        BattleParticipantDisplay firstSelectableEnemyDisplay = null;
        if (action.TargetSelf)
        {
            playerDisplay.SetTargetButtonActive(true);
            firstSelectableEnemyDisplay = playerDisplay;
        }
        else
        {
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
        }

        EventSystem.current.SetSelectedGameObject(firstSelectableEnemyDisplay.gameObject);

        if (action.AreaOfEffect)
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
        playerDisplay.SetTargetButtonActive(false);

        List<BattleParticipant> targets = GetTargets(targetIndex);
        player.ChoosePlayerAction(actions[chosenAction], targets);
    }

    private void PositionSelectionIndicator(GameObject targetObject, GameObject currentSelectionIndicator)
    {
        currentSelectionIndicator.transform.position = targetObject.transform.position + Vector3.left * 60;
        currentSelectionIndicator.SetActive(true);
    }

    private void ChooseRandomAction()
    {
        HideSelectionIndicators();
        List<BattleParticipant> targets = GetTargets(Random.Range(0, enemies.Count - 1));
        player.ChoosePlayerAction(actions[Random.Range(0, actions.Count - 1)], targets);
    }

    private List<BattleParticipant> GetTargets(int targetIndex)
    {
        BattleAction action = actions[chosenAction];
        List<BattleParticipant> targets = new List<BattleParticipant>();
        if (action.TargetSelf)
        {
            targets.Add(player);
        }
        else if (action.AreaOfEffect)
        {
            targets = enemies;
        }
        else
        {
            targets.Add(enemies[targetIndex]);
        }
        return targets;
    }

    private void HideSelectionIndicators()
    {
        EventSystem.current.SetSelectedGameObject(null);
        areaOfEffectSelectionIndicators.ForEach(indicator => indicator.SetActive(false));
        useAreaOfEffectIndicators = false;
    }
}
