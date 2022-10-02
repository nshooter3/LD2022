using UnityEngine;

public class BattleUIInterference : MonoBehaviour
{
    protected GameObject canvas;

    public void StartInterference(GameObject canvas)
    {
        this.canvas = canvas;
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
}
