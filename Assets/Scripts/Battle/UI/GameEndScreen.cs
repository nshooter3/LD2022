using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEndScreen : MonoBehaviour
{
    [SerializeField]
    private float duration;
    private float durationTimer;
    [SerializeField]
    private string nextScene;

    private void Start()
    {
        durationTimer = duration;
    }

    private void Update()
    {
        durationTimer -= Time.deltaTime;
        if (durationTimer <= 0)
        {
            SceneManager.LoadScene(nextScene);
        }
    }
}
