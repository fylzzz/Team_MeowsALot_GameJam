using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class LevelFinishTrigger : MonoBehaviour
{
    [Header("End Panel")]
    [SerializeField] private CanvasGroup endPanelCanvas;
    [SerializeField] private GameObject endMainMenuButton; // optional, for controller/keyboard focus
    [SerializeField] private TMP_Text finalTimeText;       // optional
    [SerializeField] private float fadeDuration = 1f;

    [Header("Effects")]
    public ParticleSystem[] finishParticles;
    public AudioSource winSFX;

    [Header("References")]
    [SerializeField] private Timer levelTimer;

    private bool finished;

    private void OnTriggerEnter(Collider other)
    {
        if (finished) return;
        if (!other.CompareTag("Player")) return;
        finished = true;

        if (winSFX != null) winSFX.Play();

        foreach (ParticleSystem ps in finishParticles)
            ps.Play();

        CompleteLevel();
    }

    private void CompleteLevel()
    {
        if (levelTimer != null)
        {
            levelTimer.StopTimer();

            if (finalTimeText != null)
            {
                System.TimeSpan t = System.TimeSpan.FromSeconds(levelTimer.GetTime());
                finalTimeText.text = string.Format("{0:00}:{1:00}:{2:00}",
                    t.Minutes, t.Seconds, t.Milliseconds / 10);
            }
        }

        endPanelCanvas.gameObject.SetActive(true);
        endPanelCanvas.interactable = true;
        endPanelCanvas.blocksRaycasts = true;

        if (endMainMenuButton != null)
            EventSystem.current.SetSelectedGameObject(endMainMenuButton);

        StartCoroutine(FadeInPanel());
    }

    private IEnumerator FadeInPanel()
    {
        float elapsed = 0f;
        endPanelCanvas.alpha = 0f;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.unscaledDeltaTime; // keeps fading even if timeScale is 0
            endPanelCanvas.alpha = Mathf.Clamp01(elapsed / fadeDuration);
            yield return null;
        }

        endPanelCanvas.alpha = 1f;
    }
}