public class FMODEventsAndParameters
{
    public const string ENCOUNTER_CONTROLLER = "encounter_controller";
    public const string COUNTDOWN_TIMER = "countdown_timer";
    public const string FINAL_BOSS = "boss_fight";


    #region FMOD Events

    // Battle
    public const string COUNTDOWN_SELECTION_FAIL = "event:/sfx/battle/countdown_selection_fail";
    public const string COUNTDOWN_TICK = "event:/sfx/battle/countdown_tick";
    public const string PLAYER_DEATH = "event:/sfx/abilities/player/misc/player_death";
    public const string ENEMY_DEATH = "event:/sfx/abilities/enemy/misc/enemy_death";


    // UI
    public const string CURSOR_SELECT = "event:/ui/cursor_select";
    public const string CURSOR_DESELECT = "event:/ui/cursor_deselect";
    public const string CURSOR_MOVE = "event:/ui/cursor_move";
    public const string ENEMY_SELECT_CURSOR_SELECT = "event:/ui/enemy_select_cursor_select";
    public const string ENEMY_SELECT_CURSOR_MOVE = "event:/ui/enemy_select_cursor_move";
    public const string POP_UP_OPEN = "event:/ui/pop_up_open";
    public const string POP_UP_CLOSE = "event:/ui/pop_up_close";

    #endregion
}

namespace FMODParamValues
{
    public enum EncounterControllerValues
    {
        StartBattle,
        Idle,
        Action,
        EnemyDefeated,
        PlayerDies
    }
}