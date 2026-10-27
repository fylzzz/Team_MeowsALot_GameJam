using UnityEngine;
using System.Collections;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] TMP_Text timerText;
    private float gameTime;
    private Coroutine timerRoutine;

    [Header("countdown")]
    [SerializeField] TMP_Text countdownText;
    [SerializeField] PlayerController player;
    [SerializeField] string startMessage = "Monster Mash";


    [Header("countdownFade")]
    [SerializeField] float fadeDuration = 0.3f;  
    [SerializeField] float stepTime = 1f;        // time each message is on screen in total
    [SerializeField] float smallScale = 0.5f;    // size multiplier when invisible (1 = normal)

    [Header("SlimeDrop")]
    [SerializeField] RectTransform SlimeUI;
    [SerializeField] float slideDistance = 250f; //how far it hides above 
    [SerializeField] float duration = 1.5f;

    private RectTransform rect;
    private Vector2 showPos;
    private Vector2 hiddenPos;
    private Coroutine slideRoutine;

    
    public bool playerStart {get; private set;}
    private Vector3 baseScale;


    private void Awake()
    {
        rect = SlimeUI;
        showPos = rect.anchoredPosition;
        hiddenPos = showPos + Vector2.up * slideDistance;
        rect.anchoredPosition = hiddenPos;
    }
    private void Start()
    {
        baseScale = countdownText.transform.localScale;
        countdownText.alpha = 0f;
        StartCoroutine(CountDownRoutine());
    }

    public void Show()
    {
        Slide(showPos);
    }
    public void Hide()
    {
        Slide(hiddenPos);
    }    

    void Slide(Vector2 target)
    {
        if (slideRoutine != null) StopCoroutine(slideRoutine);
        slideRoutine = StartCoroutine(SlimeSlideRoutine(target));

    }
    public void StartTimer()
    {
        if (timerRoutine != null) return;
        timerRoutine = StartCoroutine(TimerRoutine());
    }

    public void StopTimer()
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

    IEnumerator CountDownRoutine()
    {
        playerStart = false;
        ResetTime();
        player.Freeze();

        yield return FadeIn("3");
        yield return FadeIn("2");
        yield return FadeIn("1");

        playerStart = true;
        player.UnFreeze();
        StartTimer();
        Show();

        yield return FadeIn(startMessage);
        countdownText.text = "";

    }

    IEnumerator FadeIn(string message)
    {
        countdownText.text = message;
        float hold = Mathf.Max(0f, stepTime - fadeDuration * 2f);

        yield return fade(0f, 1f, smallScale, 1f);
        yield return new WaitForSeconds(hold);
        yield return fade(1f, 0f, 1f, smallScale);
    }
    IEnumerator fade(float fromAlpha, float toAlpha, float fromScale, float toScale)
    {
        float elapsed = 0f;

        while(elapsed < fadeDuration)
        {
            elapsed += Time.unscaledDeltaTime;
            float t = Mathf.Clamp01(elapsed/fadeDuration);

            countdownText.alpha = Mathf.Lerp(fromAlpha, toAlpha, t);
            countdownText.transform.localScale = baseScale * Mathf.Lerp(fromScale, toScale, t);

            yield return null;
        }

        countdownText.alpha = toAlpha;
        countdownText.transform.localScale = baseScale * toScale;
    }

    IEnumerator SlimeSlideRoutine(Vector2 target)
    {
        Vector2 start = rect.anchoredPosition;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.unscaledDeltaTime;
            float t = Mathf.SmoothStep(0f, 1f, elapsed / duration);
            rect.anchoredPosition = Vector2.Lerp(start, target, t);
            yield return null;
        }

        rect.anchoredPosition = target;
    }
}
