using System.Collections.Generic;
using UnityEngine;

public abstract class BattleAction : MonoBehaviour
{
    [SerializeField]
    private string actionName;
    public string ActionName { get { return actionName; } }

    [SerializeField]
    private string spanishActionName;

    private string noVowelsName;
    private string noConsonantsName;

    /// <summary>
    /// The cost of MP per Attack.
    /// </summary>
    [SerializeField]
    protected int mpCost;
    public int MpCost { get { return mpCost; } }

    [SerializeField]
    private bool targetSelf;
    public bool TargetSelf { get { return targetSelf; } }

    [SerializeField]
    private bool areaOfEffect;
    public bool AreaOfEffect { get { return areaOfEffect; } }

    [SerializeField]
    private ActionAnimation battleAnimation;

    [SerializeField]
    private string description;
    public string Description { get { return description; } }

    public string currentName;

    private void Start()
    {
        noVowelsName = RandomUtil.ReplaceVowels(ActionName);
        noConsonantsName = RandomUtil.ReplaceConsonants(ActionName);
        SetDefaultName();
    }

    public void SetDefaultName()
    {
        currentName = actionName;
    }

    public void SetSpanishName()
    {
        currentName = spanishActionName;
    }

    public void SetNoVowelsName()
    {
        currentName = noVowelsName;
    }

    public void SetNoConsonantsName()
    {
        currentName = noConsonantsName;
    }

    /// <summary>
    /// The Recoil Damage the attack does.
    /// </summary>
    [SerializeField]
    protected int recoil;
    public int Recoil { get { return recoil; } }

    public void RunAction(BattleParticipant user, BattleParticipant target)
    {
        OnRunAction(user, target);
        if (mpCost > 0)
        {
            user.DrainMp(mpCost);
        }
    }

    protected abstract void OnRunAction(BattleParticipant user, BattleParticipant target);

    public abstract string GetIntentDisplay();

    public virtual BattleAction InstantiateAction(BattleParticipant user)
    {
        return Instantiate<BattleAction>(this, user.transform);
    }

    public List<BattleAnimation> InstantiateAnimations(BattleParticipant user, List<BattleParticipant> targets, ActionAnimation textAnimation)
    {
        List<BattleAnimation> animations = new List<BattleAnimation>();
        animations.Add(InstantiateAnimation(user, targets, textAnimation));
        animations.Add(InstantiateAnimation(user, targets, battleAnimation));
        animations.Add(gameObject.AddComponent<UpdateStatDisplayAnimation>());
        return animations;
    }


    private ActionAnimation InstantiateAnimation(BattleParticipant user, List<BattleParticipant> targets, ActionAnimation animationPrefab)
    {
        if (battleAnimation == null)
        {
            return null;
        }
        ActionAnimation animation = Instantiate<ActionAnimation>(animationPrefab);
        animation.action = this;
        animation.gameObject.SetActive(false);
        animation.SetParticipants(user, targets);
        return animation;
    }
}
