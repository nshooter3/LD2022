using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneSelectDirector : MenuBase
{
    // Assets (Might migrate UI assets specifically to its own script so this is only focused on Director)
    private List<EnemyEncounter> encounters;
    [SerializeField] GameObject animatingIconGroup;
    private List<Transform> animatingIconTransforms;

    // Director
    private PlayableDirector director;
    [SerializeField] private PlayableAsset goLeftTimeline, goRightTimeline;

    // Menu Control Variables
    private int iconIndex;
    [SerializeField] List<CharacterSelectIcon> playerIcons;

    [SerializeField]
    private string battleScene;

    [SerializeField]
    private Color lockColor;
    [SerializeField]
    private Color completedColor;

    void Start()
    {
        director = GetComponent<PlayableDirector>();
        director.playOnAwake = false;
        animatingIconTransforms = new List<Transform>();

        foreach (RectTransform child in animatingIconGroup.transform)
        {
            animatingIconTransforms.Add(child);
        }
        iconIndex = 0;

        encounters = BattleOrchestrator.Instance.GetAvailableEncounters();

        UpdateRoster(director);
    }

    protected override void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) && encounters.Count > 1)
        {
            if (iconIndex == 0) { iconIndex = encounters.Count - 1; }
            else { iconIndex = Mathf.Clamp(iconIndex - 1, 0, encounters.Count); }
            animatingIconGroup.SetActive(true);
            foreach (CharacterSelectIcon icon in playerIcons)
            {
                icon.gameObject.SetActive(false);
            }
            PlayLeftScroll();
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) && encounters.Count > 1)
        {
            if (iconIndex == (encounters.Count - 1)) { iconIndex = 0; }
            else { iconIndex = Mathf.Clamp(iconIndex + 1, 0, encounters.Count); }
            animatingIconGroup.SetActive(true);
            foreach (CharacterSelectIcon icon in playerIcons)
            {
                icon.gameObject.SetActive(false);
            }
            PlayRightScroll();
        }
        if (Input.GetButtonDown("Submit") && (!encounters[iconIndex].FinalBoss || BattleOrchestrator.Instance.finalBossUnlocked))
        {
            BattleOrchestrator.Instance.currentEncounter = encounters[iconIndex];
            FMODUnity.RuntimeManager.PlayOneShot(FMODEventsAndParameters.ENEMY_SELECT_CURSOR_SELECT);
            ChangeScene(battleScene);
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

        if (nextIndex == encounters.Count) { nextIndex = 0; }
        if (previousIndex < 0) { previousIndex = encounters.Count - 1; }

        UpdateIcon(playerIcons[0], encounters[previousIndex]);
        UpdateIcon(playerIcons[1], encounters[iconIndex]);
        UpdateIcon(playerIcons[2], encounters[nextIndex]);

        animatingIconGroup.SetActive(false);
        if (encounters.Count > 1)
        {
            foreach (CharacterSelectIcon icon in playerIcons)
            {
                icon.gameObject.SetActive(true);
            }
        }
        else
        {
            playerIcons[0].gameObject.SetActive(false);
            playerIcons[1].gameObject.SetActive(true);
            playerIcons[2].gameObject.SetActive(false);
        }
    }

    private void UpdateIcon(CharacterSelectIcon icon, EnemyEncounter encounter)
    {
        Color color = Color.white;
        if (BattleOrchestrator.Instance.EncounterCompleted(encounter))
        {
            color = completedColor;
        }
        else if (encounter.FinalBoss && !BattleOrchestrator.Instance.finalBossUnlocked)
        {
            color = lockColor;
        }
        icon.UpdateIcon(encounter.CharacterSprite, color);
    }
}