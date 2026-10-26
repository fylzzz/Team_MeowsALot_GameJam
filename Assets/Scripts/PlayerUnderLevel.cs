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

    private bool resetting;

    private void Start()
    {
        respawnPoint.transform.position = player.transform.position;
        resetCanvas.alpha = 0f;
    }

    private void Update()
    {
        if (resetting) return;
        if (player.transform.position.y < bottomOfMap.transform.position.y)
        {
            StartCoroutine(fade(1f, 0f));
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
            resetPlayerPosition();
            resetting = false;
            yield return null;
        }
        resetCanvas.alpha = to;
    }
}
