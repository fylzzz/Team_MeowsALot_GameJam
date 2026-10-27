using UnityEngine;

public class Monsters : MonoBehaviour
{
    [SerializeField] float sizeIncrease = 0.2f;
    [SerializeField] float spinSpeed = 90f;

    [Header("bobbing")]
    [SerializeField] float bobHeight = 0.25f;
    [SerializeField] float bobSpeed = 2f;

    private Vector3 startPos;
    private float bobOffset;

    public MonstersInLevel monstersInLevel;

    private void Start()
    {
        if (monstersInLevel == null)
        {
            monstersInLevel = FindObjectOfType<MonstersInLevel>();
        }
        startPos = transform.position;
        bobOffset = Random.Range(0, Mathf.PI * 2f); //monsters no bob in sync with this
    }

    private void Update()
    {
        transform.Rotate(0f, spinSpeed * Time.deltaTime, 0f, Space.World);

        float newY = startPos.y + Mathf.Sin(Time.time * bobSpeed + bobOffset) * bobHeight;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
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

        monstersInLevel.monsterCount++;
    }
}
