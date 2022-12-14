using System;
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
    public List<BattleAction> actions;
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

    [SerializeField]
    private List<Image> actionMenuImages;

    [SerializeField]
    private ActionDescriptions actionDescriptions;

    private Queue<BattleAnimation> animationQueue = new Queue<BattleAnimation>();

    private List<BattleUIInterference> interferences = new List<BattleUIInterference>();

    public bool AnimationsComplete { get { return animationQueue.Count == 0; } }

    private int chosenActionIndex;

    private bool actionSelectionOverridden;

    public float CanvasScale { get { return transform.localScale.x; } }

    private void Awake()
    {
        instance = this;
    }

    protected override void Update()
    {
        base.Update();
        SetActionDescription();
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
                BattleAction enemyAction = enemies[i].currentAction;
                enemyDisplays[i].SetIntent(enemyAction.GetIntentType(), enemyAction.GetIntentText());
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
        chosenActionIndex = -1;

        actionSelectionOverridden = false;
        foreach (BattleUIInterference interference in interferences)
        {
            actionSelectionOverridden = interference.OverrideActionSelection() || actionSelectionOverridden;
        }
        if (actionSelectionOverridden)
        {
            ToggleMenuImages(false);
        }
        else
        {
            actions = RandomUtil.ChooseRandomElementsFromList(actions, actions.Count);
            ToggleMenuImages(true);
            Button firstSelectableAction = null;
            for (int i = 0; i < actionButtons.Count; i++)
            {
                Button button = actionButtons[i];
                if (i < actions.Count)
                {
                    BattleAction action = actions[i];
                    button.gameObject.SetActive(true);
                    button.GetComponentInChildren<TextMeshProUGUI>().text = action.currentName;

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
        }

        interferences.ForEach(interference => interference.StartInterference());

        moveTimer.StartTimer(ChooseDefaultAction);
    }

    public void UpdateButtonNames()
    {
        for (int i = 0; i < actionButtons.Count; i++)
        {
            Button button = actionButtons[i];
            if (i < actions.Count)
            {
                BattleAction action = actions[i];
                button.GetComponentInChildren<TextMeshProUGUI>().text = action.currentName;
            }
        }
    }

    public void SetButtonsInverted(bool inverted)
    {
        for (int i = 0; i < actionButtons.Count; i++)
        {
            Button button = actionButtons[i];
            ActionButton actionButton = button.GetComponent<ActionButton>();
            if (actionButton != null)
            {
                actionButton.SetInverted(inverted);
            }
        }
    }

    public void SetActionDescription()
    {
        if (EventSystem.current.currentSelectedGameObject != null && !actionSelectionOverridden)
        {
            ActionButton currentButton = EventSystem.current.currentSelectedGameObject.GetComponent<ActionButton>();
            if (currentButton != null)
            {
                actionDescriptions.SetAction(actions[currentButton.index]);
            }
        }
    }

    public void ChooseAction(int actionIndex)
    {
        if (!ShouldAllowActionInput())
        {
            return;
        }

        PlaySelectSound();

        chosenActionIndex = actionIndex;
        BattleAction action = actions[chosenActionIndex];

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
        ToggleMenuImages(false);
    }

    public void ChooseTarget(int targetIndex)
    {
        if (!ShouldAllowActionInput())
        {
            return;
        }

        HideSelectionIndicators();
        enemyDisplays.ForEach(display => display.SetTargetButtonActive(false));
        playerDisplay.SetTargetButtonActive(false);

        List<BattleParticipant> targets = GetTargets(targetIndex);
        player.ChoosePlayerAction(actions[chosenActionIndex], targets);
        FMODUnity.RuntimeManager.PlayOneShot(FMODEventsAndParameters.CURSOR_SELECT);
    }

    public void QueueAnimation(BattleAnimation animation)
    {
        animationQueue.Enqueue(animation);
    }

    public void AddInterference(BattleUIInterference interference)
    {
        interferences.Add(interference);
    }

    public void RemoveInterference(Type interferenceType)
    {
        int interferenceIndex = interferences.FindIndex(interference => interference.GetType() == interferenceType);
        if (interferenceIndex >= 0)
        {
            interferences.RemoveAt(interferenceIndex);
        }
    }

    public GameObject GetEnemyAnchorPosition(int enemyIndex)
    {
        return enemyDisplays[enemyIndex].gameObject;
    }

    private bool ShouldAllowActionInput()
    {
        bool allowInput = true;
        foreach (BattleUIInterference interference in interferences)
        {
            allowInput = interference.OnActionSelectInput() && allowInput;
        }
        return allowInput;
    }

    private void PositionSelectionIndicator(GameObject targetObject, GameObject currentSelectionIndicator)
    {
        currentSelectionIndicator.transform.position = targetObject.transform.position + Vector3.left * 100 * CanvasScale;
        currentSelectionIndicator.SetActive(true);
    }

    private void ChooseDefaultAction()
    {
        List<int> eligibleActions = new List<int>();
        if (chosenActionIndex < 0)
        {
            for (int i = 0; i < actionButtons.Count; i++)
            {
                if (EventSystem.current.currentSelectedGameObject == actionButtons[i].gameObject)
                {
                    chosenActionIndex = i;
                    break;
                }
            }
        }
        
        if (chosenActionIndex < 0)
        {
            for (int i = 0; i < actions.Count; i++)
            {
                if (actionButtons[i].interactable)
                {
                    eligibleActions.Add(i);
                }
            }
            chosenActionIndex = RandomUtil.GetRandomElementFromList(eligibleActions);
        }
        BattleAction chosenAction = actions[chosenActionIndex];

        List<int> eligibleEnemies = new List<int>();
        int chosenTargetIndex = -1;
        for (int i = 0; i < enemyDisplays.Count; i++)
        {
            if (EventSystem.current.currentSelectedGameObject == enemyDisplays[i].TargetButton.gameObject)
            {
                chosenTargetIndex = i;
                break;
            }
        }
        if (chosenTargetIndex < 0)
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                if (!enemies[i].Dead)
                {
                    eligibleEnemies.Add(i);
                }
            }
            chosenTargetIndex = RandomUtil.GetRandomElementFromList(eligibleEnemies);
        }
        List<BattleParticipant> chosenTargets = GetTargets(chosenTargetIndex);

        HideSelectionIndicators();

        player.ChoosePlayerAction(chosenAction, chosenTargets);
    }

    private List<BattleParticipant> GetTargets(int targetIndex)
    {
        BattleAction action = actions[chosenActionIndex];
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
        foreach (BattleParticipantDisplay enemyDisplay in enemyDisplays)
        {
            enemyDisplay.SetIntent(IntentType.NONE, "");
        }
        foreach (BattleUIInterference interference in interferences)
        {
            interference.OnActionSelectionEnd();
        }
        ToggleMenuImages(false);
    }

    public BattleParticipantDisplay FindDisplayForParticipant(BattleParticipant participant)
    {
        if (participant == player)
        {
            return playerDisplay;
        }
        return enemyDisplays[enemies.FindIndex(p => p == participant)];
    }

    private void ToggleMenuImages(bool enabled)
    {
        actionMenuImages.ForEach(image => image.gameObject.SetActive(enabled));
    }

    public void SetActionsDefaultName()
    {
        actions.ForEach(p => p.SetDefaultName());
    }

    public void SetActionsSpanishName()
    {
        actions.ForEach(p => p.SetSpanishName());
    }

    public void SetActionsNoVowelsName()
    {
        actions.ForEach(p => p.SetNoVowelsName());
    }

    public void SetActionsNoConsonantsName()
    {
        actions.ForEach(p => p.SetNoConsonantsName());
    }
}