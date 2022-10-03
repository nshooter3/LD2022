using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NewActionMenu : MenuBase
{
    [SerializeField]
    private List<Button> actionButtons;
    private List<BattleAction> actionChoices;
    [SerializeField]
    private string nextScene;

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
}
