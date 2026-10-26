using UnityEngine;

public class Monsters : MonoBehaviour
{
    [SerializeField] float sizeIncrease = 0.2f;
    [SerializeField] float spinSpeed = 90f;

    private void Update()
    {
        transform.Rotate(0f, spinSpeed * Time.deltaTime, 0f, Space.World);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        var player = other.GetComponentInParent<PlayerController>();
        if (player == null) return;

        player.increaseSize(sizeIncrease);

        // stop the monster affecting physics once attached
        foreach (var col in GetComponentsInChildren<Collider>())
            col.enabled = false;

        var monsterRb = GetComponent<Rigidbody>();
        if (monsterRb != null) Destroy(monsterRb);

        enabled = false; // stop the spin
        transform.SetParent(player.transform);
    }
}
