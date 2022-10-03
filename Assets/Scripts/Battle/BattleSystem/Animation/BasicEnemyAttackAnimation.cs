using UnityEngine;

public class BasicEnemyAttackAnimation : ActionAnimation
{
    [SerializeField]
    private float duration;
    private float durationTimer;

    [SerializeField]
    public enum PlayerAnimationType { Attack, Spell }
    public PlayerAnimationType playerAnimationType = PlayerAnimationType.Attack;

    private EnemyAnimator anim;

    protected override void OnAnimationStart()
    {
        durationTimer = duration;
        anim = user.GetComponentInChildren<EnemyAnimator>();
        if (anim != null)
        {
            switch (playerAnimationType)
            {
                case PlayerAnimationType.Attack:
                    anim.Attack();
                    break;
                case PlayerAnimationType.Spell:
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
        if (targets[0].Dead)
        {
            targets[0].GetComponentInChildren<PlayerAnimator>()?.Die();
        }
        else
        {
            targets[0].GetComponentInChildren<PlayerAnimator>()?.Hurt();
        }
    }
}
