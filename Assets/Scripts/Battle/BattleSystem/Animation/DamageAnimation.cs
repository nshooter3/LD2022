using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageAnimation : BattleAnimation
{
    [SerializeField]
    private float duration;
    private float durationTimer;

    [SerializeField]
    private TextMeshProUGUI textPrefab;
    [SerializeField]
    private Color hpDamageColor;
    [SerializeField]
    private Color hpHealColor;
    [SerializeField]
    private Color mpDrainColor;
    [SerializeField]
    private Color mpHealColor;
    [SerializeField]
    private float damageOffset;
    [SerializeField]
    private float mpOffset;

    public List<DamageAnimationRecord> damageAnimationRecords;

    protected override void OnAnimationStart()
    {
        durationTimer = duration;
        transform.parent = BattleUI.instance.transform;
        foreach (DamageAnimationRecord record in damageAnimationRecords)
        {
            TextMeshProUGUI text = Instantiate<TextMeshProUGUI>(textPrefab, transform);
            if (record.damage >= 0)
            {
                text.color = record.damageType == DamageAnimationRecord.DamageType.HP ? hpDamageColor : mpDrainColor;
                text.text = record.damage.ToString();
            }
            else
            {
                text.color = record.damageType == DamageAnimationRecord.DamageType.HP ? hpHealColor : mpHealColor;
                text.text = (-record.damage).ToString();
            }
            float offset = record.damageType == DamageAnimationRecord.DamageType.HP ? damageOffset : mpOffset;
            BattleParticipantDisplay display = BattleUI.instance.FindDisplayForParticipant(record.participant);
            text.transform.position = display.transform.position + Vector3.down * offset * BattleUI.instance.CanvasScale;
        }
    }

    public override void UpdateAnimation()
    {
        durationTimer -= Time.deltaTime;
        transform.Translate(new Vector3(0, durationTimer * 2, 0));
    }

    public override bool IsAnimationFinished()
    {
        return durationTimer <= 0;
    }

    protected override void OnAnimationEnd()
    {
        Destroy(gameObject);
    }
}
