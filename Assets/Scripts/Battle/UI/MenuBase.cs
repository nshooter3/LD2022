using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public abstract class MenuBase : MonoBehaviour
{
    [SerializeField]
    private FMODUnity.StudioEventEmitter fmodMusicEvent;

    public bool useBaseCursorAudio = true;

    private const float SCENE_CHANGE_DELAY = 1.5f;

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
        if (useBaseCursorAudio)
        {
            FMODUnity.RuntimeManager.PlayOneShot(FMODEventsAndParameters.CURSOR_MOVE);
        }
        else
        {
            FMODUnity.RuntimeManager.PlayOneShot(FMODEventsAndParameters.ENEMY_SELECT_CURSOR_MOVE);
        }
    }

    protected void PlaySelectSound()
    {
        if (useBaseCursorAudio)
        {
            FMODUnity.RuntimeManager.PlayOneShot(FMODEventsAndParameters.CURSOR_SELECT);
        }
        else
        {
            FMODUnity.RuntimeManager.PlayOneShot(FMODEventsAndParameters.ENEMY_SELECT_CURSOR_SELECT);
        }
    }

    protected void SetSelectedGameObject(GameObject gameObject)
    {
        EventSystem.current.SetSelectedGameObject(gameObject);
        previousSelectorGameObject = gameObject;
    }

    protected void ChangeScene(string newScene)
    {
        if (fmodMusicEvent != null)
        {
            fmodMusicEvent.Stop();
        }
        StartCoroutine(DelaySceneChange(newScene));
    }

    protected IEnumerator DelaySceneChange(string newScene)
    {
        yield return new WaitForSeconds(SCENE_CHANGE_DELAY);
        SceneManager.LoadScene(newScene);
    }
}
