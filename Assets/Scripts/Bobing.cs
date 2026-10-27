using UnityEngine;

public class Bobing : MonoBehaviour
{
    [SerializeField] float bobHeight = 0.25f;
    [SerializeField] float bobSpeed = 2f;

    private Vector3 startPos;
    private float bobOffset;

    private void Start()
    {
        startPos = transform.position;
        bobOffset = Random.Range(0, Mathf.PI * 2f); //monsters no bob in sync with this
    }
    private void Update()
    {
        float newY = startPos.y + Mathf.Sin(Time.time * bobSpeed + bobOffset) * bobHeight;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}
