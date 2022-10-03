using TMPro;
using UnityEngine;

public class AttackTextAnimation : ActionAnimation
{
    [SerializeField]
    private TextMeshProUGUI text;
    [SerializeField]
    private float duration;
    private float durationTimer;

    protected override void OnAnimationStart()
    {
        transform.parent = BattleUI.instance.transform;
        text.text = action.ActionName;
        durationTimer = duration;
        text.transform.position = userDisplay.transform.position + Vector3.up * 200f * BattleUI.instance.CanvasScale;
    }

    public override void UpdateAnimation()
    {
        durationTimer -= Time.deltaTime;
    }

    public override bool IsAnimationFinished()
    {
        return durationTimer <= 0;
    }

    protected override void OnAnimationEnd()
    {
    }
}
