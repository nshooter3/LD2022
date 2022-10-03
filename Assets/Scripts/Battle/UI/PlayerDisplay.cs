using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerDisplay : BattleParticipantDisplay
{
    [SerializeField]
    private Image mpBar;
    public int maxMp { private get; set; }

    [SerializeField]
    private TextMeshProUGUI mpNumbers;

    public void SetMp(int mp)
    {
        SetBarValue(mpBar, mp, maxMp);
        mpNumbers.text = mp + "/" + maxMp;
    }
}
