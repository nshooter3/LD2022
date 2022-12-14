using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Typeshift : BattleAction
{
    public override IntentType GetIntentType()
    {
        return IntentType.BUFF;
    }

    protected override void OnRunAction(BattleParticipant user, BattleParticipant target)
    {
        FinalBossParticles particles = user.GetComponentInChildren<FinalBossParticles>();
        if (user.CurrentElementType == ElementType.Fire)
        {
            user.ChangeElementalType(ElementType.Water);
            particles.SetWater();
        }
        else if (user.CurrentElementType == ElementType.Water)
        {
            user.ChangeElementalType(ElementType.Grass);
            particles.SetGrass();
        }
        else if (user.CurrentElementType == ElementType.Grass)
        {
            user.ChangeElementalType(ElementType.Fire);
            particles.SetFire();
        }
        else if (user.CurrentElementType == ElementType.Typeless)
        {
            var randomChoice = Random.Range(0, 2);
            switch (randomChoice)
            {
                case 0:
                    user.ChangeElementalType(ElementType.Fire);
                    particles.SetFire();
                    break;
                case 1:
                    user.ChangeElementalType(ElementType.Water);
                    particles.SetWater();
                    break;
                case 2:
                    user.ChangeElementalType(ElementType.Grass);
                    particles.SetGrass();
                    break;
            }
        }
    }
}
