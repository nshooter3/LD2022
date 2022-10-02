using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MenuBase
{
    [SerializeField]
    private string startScene;
    [SerializeField]
    private Button startButton;

    private void Start()
    {
        SetSelectedGameObject(startButton.gameObject);
    }

    public void StartGame()
    {
        PlaySelectSound();
        ChangeScene(startScene);
    }

    public void QuitGame()
    {
        PlaySelectSound();
        Application.Quit();
    }
}
