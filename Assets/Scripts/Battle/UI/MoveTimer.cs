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
            timerText.text = currentTime.ToString("F3");

            if (Expired)
            {
                timeExpiredAction.Invoke();
            }
        }
    }
}
