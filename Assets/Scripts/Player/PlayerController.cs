using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    [SerializeField] Rigidbody rb;
    [SerializeField] Camera camera;
    [SerializeField] float boostStrength;

    private GameObject currentCheckpoint;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        #if UNITY_EDITOR
        if (Keyboard.current.pKey.wasPressedThisFrame)
        {
            onDeath();
        }
        #endif
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Checkpoint"))
        {
            currentCheckpoint = other.gameObject;
        }
    }
}
