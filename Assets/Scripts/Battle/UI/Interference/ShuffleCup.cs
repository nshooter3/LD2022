using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShuffleCup : MonoBehaviour
{
    public int num;

    [SerializeField]
    private Animator anim;

    [SerializeField]
    private TextMeshProUGUI text;

    [SerializeField]
    private Button button;
    public Button Button { get { return button; } }

    public BattleAction battleAction;
    private CupShuffler cupShuffler;

    public void Init(BattleAction battleAction, CupShuffler cupShuffler)
    {
        this.battleAction = battleAction;
        this.cupShuffler = cupShuffler;
        text.text = battleAction.ActionName;
    }

    public void SetCovered(bool covered)
    {
        anim.SetBool("Covered", covered);
    }

    public void PickCup()
    {
        button.interactable = false;
        SetCovered(false);
        cupShuffler.PickCup(num);
    }
}
