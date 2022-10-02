using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BattleUI : MenuBase
{
    public static BattleUI instance { get; private set; }

    private BattlePlayer player;
    private List<BattleAction> actions;
    private List<Enemy> enemies;

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

    private Queue<BattleAnimation> animationQueue = new Queue<BattleAnimation>();

    public bool AnimationsComplete { get { return animationQueue.Count == 0; } }

    private int chosenAction;

    private void Awake()
    {
        instance = this;
    }

    protected override void Update()
    {
        base.Update();
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

        if (!AnimationsComplete)
        {
            BattleAnimation currentAnimation = animationQueue.Peek();
            if (!currentAnimation.started)
            {
                BattleParticipantDisplay userDisplay = null;
                List<BattleParticipantDisplay> targetDisplays = new List<BattleParticipantDisplay>();
                if (currentAnimation.user != null)
                {
                    userDisplay = FindDisplayForParticipant(currentAnimation.user);
                }
                foreach (BattleParticipant target in currentAnimation.targets)
                {
                    targetDisplays.Add(FindDisplayForParticipant(target));
                }
                currentAnimation.StartAnimation(userDisplay, targetDisplays);
            }
            else if (currentAnimation.IsAnimationFinished())
            {
                animationQueue.Dequeue();
                currentAnimation.EndAnimation();
            }
            else
            {
                currentAnimation.UpdateAnimation();
            }
        }
    }

    public void Initialize(BattlePlayer player, List<Enemy> enemies)
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
        playerDisplay.DisplayStatus(player.statuses);
        for (int i = 0; i < enemies.Count; i++)
        {
            enemyDisplays[i].SetHealth(enemies[i].currentHp);
            enemyDisplays[i].DisplayStatus(enemies[i].statuses);
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
        this.actions = actions;
        animationQueue.Enqueue(gameObject.AddComponent<DisplayActionAnimation>());
    }

    public void DisplayActionPrompt()
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

        SetSelectedGameObject(firstSelectableAction.gameObject);

        moveTimer.StartTimer(ChooseRandomAction);
    }

    public void ChooseAction(int actionIndex)
    {
        if (PopUpGenerator.instance.IsBlockingInput())
        {
            PopUpGenerator.instance.InterceptInput();
            return;
        }

        PopUpGenerator.instance.ToggleSpawnPopups(false);

        PlaySelectSound();

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

        SetSelectedGameObject(firstSelectableEnemyDisplay.gameObject);

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
        FMODUnity.RuntimeManager.PlayOneShot(FMODEventsAndParameters.CURSOR_SELECT);
    }

    public void QueueAnimation(BattleAnimation animation)
    {
        animationQueue.Enqueue(animation);
    }

    private void PositionSelectionIndicator(GameObject targetObject, GameObject currentSelectionIndicator)
    {
        currentSelectionIndicator.transform.position = targetObject.transform.position + Vector3.left * 60;
        currentSelectionIndicator.SetActive(true);
    }

    private void ChooseRandomAction()
    {
        HideSelectionIndicators();

        List<int> eligibleEnemies = new List<int>();
        List<int> eligibleActions = new List<int>();
        for (int i = 0; i < enemies.Count; i++)
        {
            if (!enemies[i].Dead)
            {
                eligibleEnemies.Add(i);
            }
        }
        for (int i = 0; i < actions.Count; i++)
        {
            if (actionButtons[i].interactable)
            {
                eligibleActions.Add(i);
            }
        }

        List<BattleParticipant> randomTargets = GetTargets(eligibleEnemies[Random.Range(0, eligibleEnemies.Count - 1)]);
        BattleAction randomAction = actions[eligibleActions[Random.Range(0, eligibleActions.Count - 1)]];
        player.ChoosePlayerAction(randomAction, randomTargets);
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
            targets = new List<BattleParticipant>(enemies);
        }
        else
        {
            targets.Add(enemies[targetIndex]);
        }
        return targets;
    }

    private void HideSelectionIndicators()
    {
        SetSelectedGameObject(null);
        areaOfEffectSelectionIndicators.ForEach(indicator => indicator.SetActive(false));
        useAreaOfEffectIndicators = false;
        moveTimer.StopTimer();
    }

    private BattleParticipantDisplay FindDisplayForParticipant(BattleParticipant participant)
    {
        if (participant == player)
        {
            return playerDisplay;
        }
        return enemyDisplays[enemies.FindIndex(p => p == participant)];
    }
}