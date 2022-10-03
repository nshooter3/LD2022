using System.Collections.Generic;
using UnityEngine;

public abstract class BattleAnimation : MonoBehaviour
{
    public bool started { get; private set; }

    public BattleParticipant user { get; private set; }
    public List<BattleParticipant> targets { get; private set; } = new List<BattleParticipant>();
    protected BattleParticipantDisplay userDisplay;
    protected List<BattleParticipantDisplay> targetDisplays;

    public void SetParticipants(BattleParticipant user, List<BattleParticipant> targets)
    {
        this.user = user;
        this.targets = targets;
    }

    public virtual void StartAnimation(BattleParticipantDisplay userDisplay, List<BattleParticipantDisplay> targetDisplays)
    {
        started = true;
        this.userDisplay = userDisplay;
        this.targetDisplays = targetDisplays;
        OnAnimationStart();
    }

    protected abstract void OnAnimationStart();

    public abstract void UpdateAnimation();

    public abstract bool IsAnimationFinished();

    public virtual void EndAnimation()
    {
        OnAnimationEnd();
    }

    protected abstract void OnAnimationEnd();
}
