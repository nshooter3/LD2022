using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BattleController : MonoBehaviour
{
    public static BattleController instance { get; private set; }

    [SerializeField]
    private BattlePlayer player;
    [SerializeField]
    private List<BattleParticipant> enemies;

    private List<BattleParticipant> battleParticipants = new List<BattleParticipant>();
    public List<BattleParticipant> aliveEnemies { get { return enemies.FindAll(participant => !participant.Dead); } }

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

        battleParticipants.ForEach(participant => participant.Initialize());
        BattleUI.instance.Initialize(player, enemies);
        ChooseActions();
    }

    public void ChooseActions()
    {
        battleParticipants.ForEach(participant => participant.ChooseAction());
    }

    public void RunBattleTurn()
    {

        foreach (BattleParticipant target in player.targets)
        {
            RunAction(player, target);
        }

        foreach (BattleParticipant enemy in enemies)
        {
            RunAction(enemy, player);
        }

        BattleUI.instance.UpdateHealth(player, enemies);

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
    }

    private void WinBattle()
    {
        Debug.Log("You won!");
        battleEnded = true;
    }
}
