using UnityEngine;

public class RotateAroundPoint : MonoBehaviour
{
    public Transform pointToRotateAround;
    public float rotationSpeed = 10f;

    void Update()
    {
        if (pointToRotateAround != null)
        {
            transform.RotateAround(pointToRotateAround.position, Vector3.up, rotationSpeed * Time.deltaTime);
        }
    }
}
