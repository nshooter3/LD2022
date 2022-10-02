using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class SceneSelectDirector : MonoBehaviour
{
    // Assets (Might migrate UI assets specifically to its own script so this is only focused on Director)
    [SerializeField] List<Sprite> characterSprites;
    [SerializeField] GameObject animatingIconGroup;
    private List<Transform> animatingIconTransforms;

    // Director
    private PlayableDirector director;
    [SerializeField] private PlayableAsset goLeftTimeline, goRightTimeline;
    
    // When Finished, Callback:
    private Action onFinished = null;

    // Menu Control Variables
    private int iconIndex;
    [SerializeField] List<CharacterSelectIcon> playerIcons;

    void Awake()
    {
        director = GetComponent<PlayableDirector>();
        director.playOnAwake = false;
        animatingIconTransforms = new List<Transform>();

        foreach (RectTransform child in animatingIconGroup.transform)
        {
            animatingIconTransforms.Add(child);
        }
        iconIndex = 0;
        UpdateRoster(director);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (iconIndex == 0) { iconIndex = characterSprites.Count - 1; }
            else { iconIndex = Mathf.Clamp(iconIndex - 1, 0, characterSprites.Count); }
            animatingIconGroup.SetActive(true);
            foreach (CharacterSelectIcon icon in playerIcons)
            {
                icon.gameObject.SetActive(false);
            }
            PlayLeftScroll();
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (iconIndex == (characterSprites.Count - 1)) { iconIndex = 0; }
            else { iconIndex = Mathf.Clamp(iconIndex + 1, 0, characterSprites.Count); }
            animatingIconGroup.SetActive(true);
            foreach (CharacterSelectIcon icon in playerIcons)
            {
                icon.gameObject.SetActive(false);
            }
            PlayRightScroll();
        }
    }

    public void PlayLeftScroll()
    {
        director.stopped += UpdateRoster;
        director.Play(goLeftTimeline);
        FMODUnity.RuntimeManager.PlayOneShot(FMODEventsAndParameters.ENEMY_SELECT_CURSOR_MOVE);
    }

    public void PlayRightScroll()
    {
        director.stopped += UpdateRoster;
        director.Play(goRightTimeline);
        FMODUnity.RuntimeManager.PlayOneShot(FMODEventsAndParameters.ENEMY_SELECT_CURSOR_MOVE);
    }

    private void UpdateRoster(PlayableDirector director)
    {
        int previousIndex = iconIndex - 1;
        int nextIndex = iconIndex + 1;

        director.stopped -= UpdateRoster;

        int i = 0;
        foreach (Transform child in animatingIconGroup.transform)
        {
            child.position = animatingIconTransforms[i].position;
            child.localScale = animatingIconTransforms[i].localScale;
            i++;
        }

        if (nextIndex == characterSprites.Count) { nextIndex = 0; }
        if (previousIndex < 0) { previousIndex = characterSprites.Count - 1; }

        playerIcons[0].UpdateIcon(characterSprites[previousIndex]);
        playerIcons[1].UpdateIcon(characterSprites[iconIndex]);
        playerIcons[2].UpdateIcon(characterSprites[nextIndex]);

        animatingIconGroup.SetActive(false);
        foreach (CharacterSelectIcon icon in playerIcons)
        {
            icon.gameObject.SetActive(true);
        }

        //onFinished?.Invoke();
    }
}