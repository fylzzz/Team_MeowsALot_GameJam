using UnityEngine;

public class Axe : MonoBehaviour
{
    public float maxAngle = 50f;
    [SerializeField] float speed = 1f;
    public float startOffSet = 0f;
    public Vector3 swingAxis = Vector3.forward;

    Quaternion startRotation;
    private void Start()
    {
        startRotation = transform.localRotation;
    }
    private void Update()
    {
        float angle = maxAngle * Mathf.Sin((Time.time + startOffSet) * speed);
        transform.localRotation = startRotation * Quaternion.AngleAxis(angle, swingAxis);
    }
}
