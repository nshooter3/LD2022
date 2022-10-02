using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using FMODParamValues;

public class BattleController : MonoBehaviour
{
    public static BattleController instance { get; private set; }

    private const float SCENE_CHANGE_DELAY = 6f;

    [SerializeField]
    private BattlePlayer player;
    private List<Enemy> enemies;

    private List<BattleParticipant> battleParticipants = new List<BattleParticipant>();
    public List<Enemy> aliveEnemies { get { return enemies.FindAll(participant => !participant.Dead); } }

    public FMODUnity.StudioEventEmitter fmodCountdownSFX;

    private bool battleEnded;

    [SerializeField]
    private ActionAnimation attackTextAnimation;

    [SerializeField]
    private string loseScene;
    [SerializeField]
    private string winScene;
    [SerializeField]
    private string gameEndScene;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        enemies = BattleOrchestrator.Instance.currentEncounter.SpawnEnemies();
        player.SetActions(BattleOrchestrator.Instance.currentActions);
        StartBattle();
    }

    private void StartBattle()
    {
        battleParticipants.Add(player);
        enemies.ForEach(enemy => battleParticipants.Add(enemy));

        SetFMODEncounterParameter((float)EncounterControllerValues.StartBattle);

        battleParticipants.ForEach(participant => participant.Initialize());
        BattleUI.instance.Initialize(player, enemies);
        ChooseActions();
    }

    public void ChooseActions()
    {
        battleParticipants.ForEach(participant => participant.ChooseAction());
        BattleUI.instance.UpdateIntents();
        SetFMODEncounterParameter((float)EncounterControllerValues.Action);
        fmodCountdownSFX.Play();
    }

    public void RunBattleTurn()
    {
        SetFMODEncounterParameter((float)EncounterControllerValues.Idle);
        fmodCountdownSFX.Stop();
        StartCoroutine(RunBattleTurnCoroutine());
    }

    public IEnumerator RunBattleTurnCoroutine()
    {
        foreach (BattleParticipant target in player.targets)
        {
            RunAction(player, target);
        }
        player.currentAction.InstantiateAnimations(player, player.targets, attackTextAnimation).ForEach(animation => QueueAnimation(animation));
        yield return WaitForAnimationCompletion();

        foreach (BattleParticipant enemy in enemies)
        {
            bool actionSuccessful = RunAction(enemy, player);
            List<BattleParticipant> targets = new List<BattleParticipant>();
            targets.Add(player);
            if (actionSuccessful)
            {
                enemy.currentAction.InstantiateAnimations(enemy, targets, attackTextAnimation).ForEach(animation => QueueAnimation(animation));
            }
            yield return WaitForAnimationCompletion();
        }
        battleParticipants.ForEach(participant => participant.OnTurnEnd());
        BattleUI.instance.UpdateStatBars();
        yield return WaitForAnimationCompletion();

        if (player.Dead)
        {
            LoseBattle();
        }
        else if (aliveEnemies.Count() == 0)
        {
            WinBattle();
        }

        if (!battleEnded)
        {
            ChooseActions();
        }
    }

    public void QueueAnimation(BattleAnimation battleAnimation)
    {
        if (battleAnimation != null)
        {
            BattleUI.instance.QueueAnimation(battleAnimation);
        }
    }

    private bool RunAction(BattleParticipant user, BattleParticipant target)
    {
        if (!user.Dead && !target.Dead)
        {
            user.currentAction.RunAction(user, target);
            return true;
        }
        return false;
    }

    private void LoseBattle()
    {
        SetFMODEncounterParameter((float)EncounterControllerValues.PlayerDies);
        ChangeScene(loseScene);
    }

    private void WinBattle()
    {
        BattleOrchestrator.Instance.CompleteEncounter();
        SetFMODEncounterParameter((float)EncounterControllerValues.EnemyDefeated);
        if (BattleOrchestrator.Instance.currentEncounter.FinalBoss)
        {
            ChangeScene(gameEndScene);
        }
        else
        {
            ChangeScene(winScene);
        }
    }

    private void ChangeScene(string nextScene)
    {
        battleEnded = true;
        fmodCountdownSFX.Stop();
        StartCoroutine(DelaySceneChange(nextScene));
    }

    private IEnumerator DelaySceneChange(string nextScene)
    {
        yield return new WaitForSeconds(SCENE_CHANGE_DELAY);
        SceneManager.LoadScene(nextScene);
    }

    private void SetFMODEncounterParameter(float paramValue)
    {
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName(FMODEventsAndParameters.ENCOUNTER_CONTROLLER, paramValue);
    }

    private IEnumerator WaitForAnimationCompletion()
    {
        while (!BattleUI.instance.AnimationsComplete)
        {
            yield return null;
        }
    }
}