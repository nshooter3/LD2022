using UnityEngine;

public class ActionShufflingInterference : BattleUIInterference
{
    private const float SHUFFLE_TIME = 2.0f;
    private float shuffleTimer;

    public void Start()
    {
        gameObject.SetActive(false);
    }

    protected override void OnInterferenceStart()
    {
        gameObject.SetActive(true);
        shuffleTimer = SHUFFLE_TIME;
    }

    public override void OnActionSelectionEnd()
    {
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

    private void Update()
    {
        if (shuffleTimer > 0f)
        {
            shuffleTimer -= Time.deltaTime;
            if (shuffleTimer <= 0f)
            {
                shuffleTimer = SHUFFLE_TIME;
                ShuffleMenu();
            }
        }
    }

    private void ShuffleMenu()
    {
        BattleUI.instance.actions = RandomUtil.ChooseRandomElementsFromList(BattleUI.instance.actions, BattleUI.instance.actions.Count);
        BattleUI.instance.UpdateButtonNames();
    }
}
