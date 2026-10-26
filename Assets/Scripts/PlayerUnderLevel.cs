using UnityEngine;

public class PlayerUnderLevel : MonoBehaviour
{
    public Transform player;
    public Transform respawnPoint;
    public Transform bottomOfMap;

    private void Start()
    {
        respawnPoint.transform.position = player.transform.position;
    }

    private void Update()
    {
        if(player.transform.position.y < bottomOfMap.transform.position.y)
        {
            resetPlayerPosition();
        }
    }
    private void resetPlayerPosition()
    {
        player.transform.position = respawnPoint.transform.position;
        Debug.Log("Player has fallen below the level. Resetting position.");
    }
}
