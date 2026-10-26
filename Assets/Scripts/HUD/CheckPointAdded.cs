using System.Collections;
using UnityEngine;

public class CheckPointAdded : MonoBehaviour
{
    [SerializeField] private CanvasGroup checkPointCanvasGroup;
    public bool isMessageVisible = false;

    private void Start()
    {
        checkPointCanvasGroup.alpha = 0f; // Start with the canvas group invisible
    }

    IEnumerator FadeCanvasGroup(CanvasGroup canvasGroup, float startAlpha, float endAlpha, float duration)
    {
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / duration);
            yield return null;
        }
        canvasGroup.alpha = endAlpha; // Ensure the final alpha is set
    }
    public IEnumerator ShowCheckPointMessageCoroutine()
    {
        isMessageVisible = true;
        yield return StartCoroutine(FadeCanvasGroup(checkPointCanvasGroup, 0f, 1f, 0.2f)); // Fade in
        yield return new WaitForSeconds(0.5f); // Wait for 2 seconds
        yield return StartCoroutine(FadeCanvasGroup(checkPointCanvasGroup, 1f, 0f, 0.2f)); // Fade out
        isMessageVisible = false;
    }
}
