using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    [SerializeField] Rigidbody rb;
    [SerializeField] Camera camera;
    [SerializeField] float boostStrength;

    [SerializeField] float growSpeed = 2f;

    private GameObject currentCheckpoint;
    private CameraController controller;

    private Vector3 targetScale;
    private Vector2 targetDistance;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        controller = camera.GetComponent<CameraController>();
        targetScale = transform.localScale;
        targetDistance = controller.distanceMultiplier;
    }

    // Update is called once per frame
    void Update()
    {
        #if UNITY_EDITOR
        if (Keyboard.current.pKey.wasPressedThisFrame)
        {
            onDeath();
        }
        if (Keyboard.current.equalsKey.wasPressedThisFrame)
        {
            increaseSize(1f);
        }
        if (Keyboard.current.minusKey.wasPressedThisFrame)
        {
            increaseSize(-1f);
        }
        #endif

        float t = 1f - Mathf.Exp(-growSpeed * Time.deltaTime);
        transform.localScale = Vector3.Lerp(transform.localScale, targetScale, t);
        controller.distanceMultiplier = Vector2.Lerp(controller.distanceMultiplier, targetDistance, t);

    }

    void OnBoost()
    {
        rb.AddForce((camera.transform.forward * boostStrength), ForceMode.Impulse);
    }

    public void AddImpulse(Vector3 force)
    {
        rb.AddForce(force, ForceMode.Impulse);
    }

    public void AddWind(Vector3 force)
    {
        rb.AddForce(force, ForceMode.Force);
    }

    public void onDeath()
    {
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        transform.position = currentCheckpoint.transform.position;
    }

    public void increaseSize(float size)
    {
        targetScale += new Vector3(size, size, size);
        targetDistance += new Vector2(size, size);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Checkpoint"))
        {
            currentCheckpoint = other.gameObject;
        }
    }

    public void Freeze()
    {
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.constraints = RigidbodyConstraints.FreezeAll;
    }
    
    public void UnFreeze()
    {
        rb.constraints = RigidbodyConstraints.None;
    }

    
}