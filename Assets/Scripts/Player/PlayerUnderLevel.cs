using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerUnderLevel : MonoBehaviour
{
    public Transform player;
    public Transform respawnPoint;
    public Transform bottomOfMap;

    public PlayerController playerController;
    public Rigidbody playerRB;
    public GameObject playerObject;

    [Header("fade")]
    [SerializeField] CanvasGroup resetCanvas;
    [SerializeField] private float fadeDuration = 0.5f;
    [SerializeField] GameObject fadeCanvas;
    [SerializeField] float fadeTime;

    private bool resetting;

    private void Start()
    {
        respawnPoint.transform.position = player.transform.position;
        resetCanvas.alpha = 0f;
        fadeCanvas.transform.localScale = new Vector3(20, 20, 20);
    }

    private void Update()
    {
        if (resetting) return;
        if (player.transform.position.y < bottomOfMap.transform.position.y)
        {
            StartCoroutine(fateToandFrom());
        }
    }
    private void resetPlayerPosition()
    {
        player.transform.position = respawnPoint.transform.position;
        playerRB.linearVelocity = Vector3.zero;
        Debug.Log("Player has fallen below the level. Resetting position.");
    }

    private IEnumerator fade(float from,float to)
    {
        float elapsed = 0f;
        resetting = true;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.unscaledDeltaTime;
            resetCanvas.alpha = Mathf.Lerp(from, to, elapsed / fadeDuration);
            fadeCanvas.transform.localScale = Vector3.Lerp(new Vector3(from*37, from*21, from), new Vector3(to*37, to*21, to), elapsed / fadeDuration);
            resetting = false;
            yield return null;
        }
        resetCanvas.alpha = to;
    }
    private IEnumerator fateToandFrom()
    {
        resetting = true;
        yield return StartCoroutine(fade(0f, fadeTime));
        yield return new WaitForSecondsRealtime(0.5f);
        resetPlayerPosition();
        yield return StartCoroutine(fade(fadeTime, 0f));
        resetting = false;
    }
}
