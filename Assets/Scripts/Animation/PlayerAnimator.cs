using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField]
    private Animator movementAnim, spriteAnim;

    [SerializeField]
    private DamageFlicker damageFlicker;

    public void Punch()
    {
        movementAnim.SetTrigger("Punch");
        spriteAnim.SetTrigger("Attack");
    }

    public void Kick()
    {
        movementAnim.SetTrigger("Kick");
        spriteAnim.SetTrigger("Attack2");
    }

    public void Spell()
    {
        movementAnim.SetTrigger("Spell");
        spriteAnim.SetTrigger("Cast");
    }

    public void Hurt()
    {
        movementAnim.SetTrigger("Hurt");
        damageFlicker.StartFlicker();
        spriteAnim.SetTrigger("Hurt");
    }

    public void Die()
    {
        movementAnim.SetTrigger("Die");
        spriteAnim.SetTrigger("Die");
    }
}
