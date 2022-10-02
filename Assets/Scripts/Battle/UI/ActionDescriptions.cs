using UnityEngine;
using TMPro;

public class ActionDescriptions : MonoBehaviour
{
    [SerializeField]
    private Color hpCostColor, mpCostColor;

    [SerializeField]
    private TextMeshProUGUI title, description, cost;

    public void SetAction(BattleAction attack)
    {
        string tempTitle = attack.ActionName;
        if (attack.AreaOfEffect)
        {
            tempTitle += " (multi)";
        }

        title.text = tempTitle;

        description.text = attack.Description;

        if (attack.MpCost > 0)
        {
            cost.color = mpCostColor;
            cost.text = "MP Cost: " + attack.MpCost;
        }
        else if (attack.Recoil > 0)
        {
            cost.color = hpCostColor;
            cost.text = "HP Cost: " + attack.Recoil;
        }
        else
        {
            cost.text = "";
        }
    }
}
