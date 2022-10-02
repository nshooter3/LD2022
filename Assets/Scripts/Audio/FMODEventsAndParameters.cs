public class FMODEventsAndParameters
{
    public const string ENCOUNTER_CONTROLLER = "encounter_controller";

    #region FMOD Events

    // Events

    public const string COUNTDOWN_SELECTION_FAIL = "event:/sfx/battle/countdown_selection_fail";


    // UI
    public const string CURSOR_SELECT = "event:/ui/cursor_select";
    public const string CURSOR_MOVE = "event:/ui/cursor_move";
    public const string ENEMY_SELECT_CURSOR_SELECT = "event:/ui/enemy_select_cursor_select";
    public const string ENEMY_SELECT_CURSOR_MOVE = "event:/ui/enemy_select_cursor_move";

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