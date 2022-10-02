using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using FMODParamValues;

public class BattleController : MonoBehaviour
{
    public static BattleController instance { get; private set; }

    [SerializeField]
    private BattlePlayer player;
    [SerializeField]
    private List<BattleParticipant> enemies;

    private List<BattleParticipant> battleParticipants = new List<BattleParticipant>();
    public List<BattleParticipant> aliveEnemies { get { return enemies.FindAll(participant => !participant.Dead); } }

    public FMODUnity.StudioEventEmitter fmodCountdownSFX;

    private bool battleEnded;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        StartBattle(enemies);
    }

    private void StartBattle(List<BattleParticipant> enemies)
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

        foreach (BattleParticipant target in player.targets)
        {
            RunAction(player, target);
        }

        foreach (BattleParticipant enemy in enemies)
        {
            RunAction(enemy, player);
        }

        battleParticipants.ForEach(participant => participant.OnTurnEnd());

        BattleUI.instance.UpdateStatBars();

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

    private void RunAction(BattleParticipant user, BattleParticipant enemy)
    {
        if (!user.Dead)
        {
            user.currentAction.RunAction(user, enemy);
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
}