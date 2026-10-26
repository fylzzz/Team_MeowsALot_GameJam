using UnityEngine;
using System.Collections;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] TMP_Text timerText;
    private float gameTime;
    private Coroutine timerRoutine;

    private void Start()
    {
        StartTimer();
    }

    public void StartTimer()
    {
        if (timerRoutine != null) return;
        timerRoutine = StartCoroutine(TimerRoutine());
    }

    private void StopTimer()
    {
        if (timerRoutine == null) return;
        StopCoroutine(timerRoutine);
        timerRoutine = null;
    }

    public void ResetTime()
    {
        gameTime = 0f;
        UpdateText();
    }

    public float GetTime() => gameTime;
    void UpdateText()
    {
        System.TimeSpan t = System.TimeSpan.FromSeconds(gameTime);
        timerText.text = string.Format("{0:00}:{1:00}:{2:00}",
            t.Minutes, t.Seconds, t.Milliseconds / 10);
    }

    IEnumerator TimerRoutine()
    {
        while (true)
        {

            gameTime += Time.deltaTime;

            //formate time as MM:SS:MS
            System.TimeSpan timeSpan = System.TimeSpan.FromSeconds(gameTime);
            timerText.text = string.Format("{0:00}:{1:00}:{2:00}",
            timeSpan.Minutes, timeSpan.Seconds, timeSpan.Milliseconds / 10);

            yield return null;
        }
    }
}
