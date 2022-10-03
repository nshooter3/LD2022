using UnityEngine;

public class ButtonInvertingInterference : BattleUIInterference
{
    public void Start()
    {
        gameObject.SetActive(false);
    }

    protected override void OnInterferenceStart()
    {
        BattleUI.instance.SetButtonsInverted(true);
        gameObject.SetActive(true);
    }

    public override void OnActionSelectionEnd()
    {
        BattleUI.instance.SetButtonsInverted(false);
        gameObject.SetActive(false);
    }

    public override void OnInterferenceEnd()
    {
    }

    public override bool OnActionSelectInput()
    {
        return true;
    }

    public override bool OverrideActionSelection()
    {
        return false;
    }
}
