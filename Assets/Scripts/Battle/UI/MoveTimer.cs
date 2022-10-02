using System;
using TMPro;
using UnityEngine;

public class MoveTimer : MonoBehaviour
{
    private TextMeshProUGUI timerText;
    private float currentTime;
    [SerializeField]
    private float timerDuration;

    private Action timeExpiredAction;

    public bool Expired { get { return currentTime <= 0; } }

    private void Start()
    {
        timerText = GetComponent<TextMeshProUGUI>();
        gameObject.SetActive(false);
    }

    public void StartTimer(Action timeExpiredAction)
    {
        gameObject.SetActive(true);
        currentTime = timerDuration;
        this.timeExpiredAction = timeExpiredAction;
    }

    public void StopTimer()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    private void Update()
    {
        if (!Expired)
        {
            currentTime = Mathf.Max(0, currentTime - Time.deltaTime);
            FMODUnity.RuntimeManager.StudioSystem.setParameterByName(FMODEventsAndParameters.COUNTDOWN_TIMER, currentTime);
            timerText.text = currentTime.ToString("F3");
            PlayCountdownTickAudio();

            if (Expired)
            {
                timeExpiredAction.Invoke();
                FMODUnity.RuntimeManager.PlayOneShot(FMODEventsAndParameters.COUNTDOWN_SELECTION_FAIL);
                FMODUnity.RuntimeManager.StudioSystem.setParameterByName(FMODEventsAndParameters.COUNTDOWN_TIMER, 10f);
            }
        }
    }

    private void PlayCountdownTickAudio()
    {
        if (currentTime <= 3f && currentTime > 0.1f)
        {
            FMODUnity.RuntimeManager.PlayOneShot(FMODEventsAndParameters.COUNTDOWN_TICK);
        }
    }
}
