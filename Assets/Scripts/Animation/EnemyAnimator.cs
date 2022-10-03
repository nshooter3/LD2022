using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    [SerializeField]
    private Animator movementAnim, spriteAnim;

    [SerializeField]
    private DamageFlicker damageFlicker;

    public void Attack()
    {
        movementAnim.SetTrigger("Attack");
        if (spriteAnim != null)
        {
            spriteAnim.SetTrigger("Attack");
        }
    }

    public void Spell()
    {
        movementAnim.SetTrigger("Spell");
        if (spriteAnim != null)
        {
            spriteAnim?.SetTrigger("Cast");
        }
    }

    public void Hurt()
    {
        movementAnim.SetTrigger("Hurt");
        damageFlicker.StartFlicker();
        if (spriteAnim != null)
        {
            spriteAnim?.SetTrigger("Hurt");
        }
    }

    public void Die()
    {
        movementAnim.SetTrigger("Die");
        if (spriteAnim != null)
        {
            spriteAnim?.SetTrigger("Die");
        }
    }
}
