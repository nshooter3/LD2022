using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class MenuBase : MonoBehaviour
{
    private GameObject previousSelectorGameObject;

    protected virtual void Update()
    {
        if (previousSelectorGameObject != EventSystem.current.currentSelectedGameObject)
        {
            PlayMoveSound();
        }
        previousSelectorGameObject = EventSystem.current.currentSelectedGameObject;
    }

    protected void PlayMoveSound()
    {
        FMODUnity.RuntimeManager.PlayOneShot(FMODEventsAndParameters.CURSOR_MOVE);
    }

    protected void PlaySelectSound()
    {
        FMODUnity.RuntimeManager.PlayOneShot(FMODEventsAndParameters.CURSOR_SELECT);
    }

    protected void SetSelectedGameObject(GameObject gameObject)
    {
        EventSystem.current.SetSelectedGameObject(gameObject);
        previousSelectorGameObject = gameObject;
    }
}
