using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    [SerializeField]
    private Animator anim;

    [SerializeField]
    private DamageFlicker damageFlicker;

    public void Attack()
    {
        anim.SetTrigger("Attack");
    }

    public void Spell()
    {
        anim.SetTrigger("Spell");
    }

    public void Hurt()
    {
        anim.SetTrigger("Hurt");
        damageFlicker.StartFlicker();
    }

    public void Die()
    {
        anim.SetTrigger("Die");
    }
}
