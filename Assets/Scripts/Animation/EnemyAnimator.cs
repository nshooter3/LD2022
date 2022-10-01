using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    [SerializeField]
    private Animator anim;

    [SerializeField]
    private DamageFlicker damageFlicker;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Attack();
        }
        else if (Input.GetKeyDown(KeyCode.X))
        {
            Spell();
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            Hurt();
        }
        else if (Input.GetKeyDown(KeyCode.V))
        {
            Die();
        }
    }

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
