using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    [SerializeField] Rigidbody rb;
    [SerializeField] Camera camera;
    [SerializeField] float boostStrength;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnBoost()
    {
        rb.AddForce((camera.transform.forward * boostStrength), ForceMode.Impulse);
    }
}
