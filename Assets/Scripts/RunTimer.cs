using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RunTimer : MonoBehaviour
{
    [SerializeField]
    Text timerText;

    [SerializeField]
    private float timerValue = 0.0f;

    float minutes;

    float seconds;

    private void Update()
    {
        if (!GameManager.isGameOver)
        {
            UpdateTimer();
            SetTextValue();
        }
    }

    private  void UpdateTimer()
    {
        timerValue += Time.deltaTime;
        minutes = Mathf.FloorToInt(timerValue / 60);
        seconds = Mathf.FloorToInt(timerValue % 60);
    }

    private void SetTextValue()
    {
        timerText.text = $"{minutes}.{seconds}";
    }
    public float GetTimerValue()
    {

        return timerValue;
    }

    public string GetFormattedTimerValue()
    {
        return $"Time : {minutes}.{seconds}";
    }

    public void ResetTimer()
    {
        timerValue = 0;
    }
}
 