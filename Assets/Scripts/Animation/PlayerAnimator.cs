using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField]
    private Animator anim;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Punch();
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            Kick();
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            Spell();
        }
        else if (Input.GetKeyDown(KeyCode.F))
        {
            Hurt();
        }
        else if (Input.GetKeyDown(KeyCode.G))
        {
            Die();
        }
    }

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
    }

    public void Die()
    {
        anim.SetTrigger("Die");
    }
}
