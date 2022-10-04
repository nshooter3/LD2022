using UnityEngine;
using TMPro;

public class ActionDescriptions : MonoBehaviour
{
    [SerializeField]
    private Color typelessColor, fireColor, waterColor, grassColor, hpCostColor, mpCostColor;

    [SerializeField]
    private TextMeshProUGUI title, description, cost, damage;

    public void SetAction(BattleAction action)
    {
        string tempTitle = action.ActionName;
        if (action.AreaOfEffect)
        {
            tempTitle += " (multi)";
        }

        title.text = tempTitle;
        title.color = typelessColor;

        description.text = action.Description;

        if (action.MpCost > 0)
        {
            cost.color = mpCostColor;
            cost.text = "MP Cost: " + action.MpCost;
        }
        else if (action.Recoil > 0)
        {
            cost.color = hpCostColor;
            cost.text = "HP Cost: " + action.Recoil;
        }
        else
        {
            cost.text = "";
        }

        if (action.ActionName == "Vampiric Jab")
        {
            damage.text = "Saps: 5";
        }
        else if (action.ActionName == "Sapping Sprouts")
        {
            damage.text = "Saps: 12";
        }
        else if (action.ActionName == "Combustion")
        {
            damage.text = "Damage: 12";
        }
        else if (action is Attack)
        {
            damage.text = "Damage: " + ((Attack)action).Damage;
            switch (((Attack)action).ElementType)
            {
                case ElementType.Fire:
                    title.color = fireColor;
                    break;
                case ElementType.Grass:
                    title.color = grassColor;
                    break;
                case ElementType.Water:
                    title.color = waterColor;
                    break;
                case ElementType.Typeless:
                    break;
            }
        }
        else if (action is Heal)
        {
            damage.text = "Heal amount: " + ((Heal)action).HealAmount;
        }
        else
        {
            damage.text = "";
        }
    }
}
