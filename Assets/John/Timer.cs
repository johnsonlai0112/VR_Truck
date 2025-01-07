using UnityEngine;
using UnityEngine.UI; // If using Text
using TMPro; // If using TextMeshPro

public class Timer : MonoBehaviour
{
    public float totalTime = 120f; // Total time in seconds (2 minutes)
    public TextMeshProUGUI timerText; // If using TextMeshPro

    private float remainingTime;
    private bool isTimerRunning = false;
    public float totalTimeUse;

    public bool timeUp = false;

    private void OnEnable()
    {
        remainingTime = totalTime;
        isTimerRunning = true;
        UpdateTimerDisplay();
    }

    void Update()
    {
        if (isTimerRunning)
        {
            if (remainingTime > 0)
            {
                remainingTime -= Time.deltaTime;
                UpdateTimerDisplay();
            }
            else
            {
                remainingTime = 0;
                isTimerRunning = false;
                TimerEnded();
            }
        }
    }

    void UpdateTimerDisplay()
    {
        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void TimerEnded()
    {
        timeUp = true;
        Debug.Log("Timer Ended!");
    }

    public void ResetTimer()
    {
        remainingTime = totalTime;
        isTimerRunning = true;
        UpdateTimerDisplay();
    }

    public void StopTimer()
    {
        totalTimeUse = totalTime - remainingTime;
        isTimerRunning = false;
 
    }
}