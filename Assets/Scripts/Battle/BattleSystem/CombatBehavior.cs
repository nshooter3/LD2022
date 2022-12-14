using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CombatBehavior : MonoBehaviour
{


    /// <summary>
    /// Chooses an action for the AI to perform.
    /// </summary>
    /// <returns>An action chosen by current attack index.</returns>
    public abstract BattleAction ChooseAction(List<BattleAction> actions);
}
