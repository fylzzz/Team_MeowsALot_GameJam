using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    PlayerUnderLevel playerUnderLevel;
    CheckPointAdded checkPointAdded;

    private void Start()
    {
        playerUnderLevel = FindObjectOfType<PlayerUnderLevel>();
        checkPointAdded = FindObjectOfType<CheckPointAdded>();
    }
    private void OnTriggerEnter(Collider checkPoint)
    {
        playerUnderLevel.respawnPoint.transform.position = checkPoint.transform.position;

        StartCoroutine(checkPointAdded.ShowCheckPointMessageCoroutine());

        Debug.Log("Checkpoint reached! Respawn point updated.");
    }
}
