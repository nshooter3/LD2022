using UnityEngine;

public class PlayerAttackAnimation : ActionAnimation
{
    [SerializeField]
    private float duration;
    private float durationTimer;

    [SerializeField]
    public enum PlayerAnimationType { Punch, Kick, Spell }
    public PlayerAnimationType playerAnimationType = PlayerAnimationType.Punch;

    private PlayerAnimator anim;

    protected override void OnAnimationStart()
    {
        durationTimer = duration;
        anim = user.GetComponentInChildren<PlayerAnimator>();
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
    }
}
