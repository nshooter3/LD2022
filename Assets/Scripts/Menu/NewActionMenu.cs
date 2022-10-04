using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NewActionMenu : MenuBase
{
    [SerializeField]
    private List<Button> actionButtons;
    private List<BattleAction> actionChoices;
    [SerializeField]
    private string nextScene;

    [SerializeField]
    private ActionDescriptions actionDescriptions;

    private void Start()
    {
        actionChoices = BattleOrchestrator.Instance.GetRandomNewActions(actionButtons.Count);
        for (int i = 0; i < actionButtons.Count; i++)
        {
            if (i >= actionChoices.Count)
            {
                actionButtons[i].gameObject.SetActive(false);
            }
            else
            {
                actionButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = actionChoices[i].ActionName;
            }
        }
        SetSelectedGameObject(actionButtons[0].gameObject);
    }

    protected override void Update()
    {
        SetActionDescription();
        base.Update();
    }

    public void ChooseAction(int actionIndex)
    {
        foreach (var button in actionButtons)
        {
            button.interactable = false;
        }
        BattleOrchestrator.Instance.AddAction(actionChoices[actionIndex]);
        LoadNextScene();
    }

    public void LoadNextScene()
    {
        PlaySelectSound();
        ChangeScene(nextScene);
    }

    public void SetActionDescription()
    {
        if (EventSystem.current.currentSelectedGameObject != null)
        {
            foreach (BattleAction action in actionChoices)
            {
                if (EventSystem.current.currentSelectedGameObject.GetComponentInChildren<TextMeshProUGUI>()?.text == action.name)
                {
                    actionDescriptions.SetAction(action);
                    break;
                }
            }
        }
    }
}
