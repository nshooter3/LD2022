using UnityEngine;
using TMPro;

public class ShuffleCup : MonoBehaviour
{
    public int num;

    [SerializeField]
    private Animator anim;

    [SerializeField]
    private TextMeshPro text;

    public BattleAction battleAction;

    public void Init(BattleAction battleAction)
    {
        this.battleAction = battleAction;
        text.text = battleAction.name;
    }

    public void SetCovered(bool covered)
    {
        anim.SetBool("Covered", covered);
    }
}
