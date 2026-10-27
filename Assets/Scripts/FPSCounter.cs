using UnityEngine;

public class FPSCounter : MonoBehaviour
{
    [SerializeField] private float updateInterval;
    [SerializeField] private TMPro.TextMeshProUGUI fpsText;

    private void Update()
    {
        InvokeRepeating(nameof(UpdateFPS), 0f, updateInterval);
    }

    private void UpdateFPS()
    {
        float fps = 1f / Time.unscaledDeltaTime;
        fpsText.text = $"FPS: {Mathf.RoundToInt(fps)}";
    }
}
