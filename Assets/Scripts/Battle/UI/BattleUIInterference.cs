using UnityEngine;

public class BattleUIInterference : MonoBehaviour
{
    public void StartInterference()
    {
        OnInterferenceStart();
    }

    protected virtual void OnInterferenceStart()
    {
    }

    public virtual void OnActionSelectionEnd()
    {
    }

    public void EndInterference()
    {
        OnInterferenceEnd();
    }

    public virtual void OnInterferenceEnd()
    {
    }

    public virtual bool OnActionSelectInput()
    {
        return true;
    }

    public virtual bool OverrideActionSelection()
    {
        return false;
    }
}
