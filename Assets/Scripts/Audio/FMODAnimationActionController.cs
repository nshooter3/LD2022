using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FMODAnimationActionController : MonoBehaviour
{
    private BattleParticipant battleParticipant;

    private void Start()
    {
        battleParticipant = GetComponentInParent<BattleParticipant>();
    }

    public void PlayAbilitySFX()
    {
        FMODUnity.RuntimeManager.PlayOneShot(battleParticipant.currentAction.fmodActionEvent);
    }
}
