using UnityEngine;

public class PlayerAttackAnimation : ActionAnimation
{
    [SerializeField]
    private float duration;
    private float durationTimer;

    [SerializeField]
    public enum PlayerAnimationType { Punch, Kick, Spell, ApplyStatus }
    public PlayerAnimationType playerAnimationType = PlayerAnimationType.Punch;

    private PlayerAnimator anim;

    protected override void OnAnimationStart()
    {
        durationTimer = duration;
        anim = user.GetComponentInChildren<PlayerAnimator>();
        if (anim != null)
        {
            switch (playerAnimationType)
            {
                case PlayerAnimationType.Punch:
                    anim.Punch();
                    break;
                case PlayerAnimationType.Kick:
                    anim.Kick();
                    break;
                case PlayerAnimationType.Spell:
                    anim.Spell();
                    break;
                case PlayerAnimationType.ApplyStatus:
                    anim.Spell();
                    break;
            }
        }
    }

    public override void UpdateAnimation()
    {
        durationTimer -= Time.deltaTime;
    }

    public override bool IsAnimationFinished()
    {
        return durationTimer <= 0;
    }

    protected override void OnAnimationEnd()
    {
        foreach (BattleParticipant target in targets)
        {
            if (target.Dead)
            {
                target.GetComponentInChildren<EnemyAnimator>()?.Die();
            }
            else if (playerAnimationType != PlayerAnimationType.ApplyStatus)
            {
                target.GetComponentInChildren<EnemyAnimator>()?.Hurt();
            }
        }
    }
}
