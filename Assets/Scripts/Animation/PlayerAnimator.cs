using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField]
    private Animator anim;

    [SerializeField]
    private DamageFlicker damageFlicker;

    public void Punch()
    {
        anim.SetTrigger("Punch");
    }

    public void Kick()
    {
        anim.SetTrigger("Kick");
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
