using UnityEngine;
using UnityEngine.UI;

public class PlayerDisplay : BattleParticipantDisplay
{
    [SerializeField]
    private Image mpBar;
    public int maxMp { private get; set; }

    public void SetMp(int mp)
    {
        SetBarValue(mpBar, mp, maxMp);
    }
}
