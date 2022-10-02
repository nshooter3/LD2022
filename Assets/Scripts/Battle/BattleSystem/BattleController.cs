using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using FMODParamValues;

public class BattleController : MonoBehaviour
{
    public static BattleController instance { get; private set; }

    [SerializeField]
    private BattlePlayer player;
    private List<Enemy> enemies;

    private List<BattleParticipant> battleParticipants = new List<BattleParticipant>();
    public List<Enemy> aliveEnemies { get { return enemies.FindAll(participant => !participant.Dead); } }

    public FMODUnity.StudioEventEmitter fmodCountdownSFX;

    private bool battleEnded;

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
        QueueAnimation(player.currentAction.InstantiateAnimation(player, player.targets));
        yield return WaitForAnimationCompletion();

        foreach (BattleParticipant enemy in enemies)
        {
            RunAction(enemy, player);
            List<BattleParticipant> targets = new List<BattleParticipant>();
            targets.Add(player);
            QueueAnimation(enemy.currentAction.InstantiateAnimation(enemy, targets));
            yield return WaitForAnimationCompletion();
        }

        battleParticipants.ForEach(participant => participant.OnTurnEnd());
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
        BattleUI.instance.QueueAnimation(battleAnimation);
    }

    private void RunAction(BattleParticipant user, BattleParticipant target)
    {
        if (!user.Dead && !target.Dead)
        {
            user.currentAction.RunAction(user, target);
        }
    }

    private void LoseBattle()
    {
        Debug.Log("You lost!");
        battleEnded = true;
        SetFMODEncounterParameter((float)EncounterControllerValues.PlayerDies);
    }

    private void WinBattle()
    {
        Debug.Log("You won!");
        battleEnded = true;
        SetFMODEncounterParameter((float)EncounterControllerValues.EnemyDefeated);
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