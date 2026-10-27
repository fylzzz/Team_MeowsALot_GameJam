using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject player;

    [Header("Settings")]
    [SerializeField] private float mouseSensitivity = 0.1f;
    [SerializeField] private float gamepadSensitivity = 180f;
    [SerializeField] public Vector2 distanceMultiplier = new Vector2(5f, 2f);

    private PlayerInput input;
    private InputAction lookAction;
    private float currentYRotation = 0f;

    public bool IsUsingGamepad { get; private set; }

    private void Awake()
    {
        input = GetComponent<PlayerInput>();
        lookAction = input.actions.FindAction("Player/Look");
    }

    private void Start()
    {

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void LateUpdate()
    {
        if (player == null || lookAction == null) return;

        InputControl control = lookAction.activeControl;
        if (control != null)
            IsUsingGamepad = control.device is Gamepad;

        Vector2 lookInput = lookAction.ReadValue<Vector2>();

        float yaw = IsUsingGamepad ? lookInput.x * gamepadSensitivity * Time.deltaTime : lookInput.x * mouseSensitivity;

        currentYRotation += yaw;
        Quaternion rotation = Quaternion.Euler(0f, currentYRotation, 0f);

        Vector3 offset = rotation * (Vector3.back * distanceMultiplier.x);

        transform.position = player.transform.position + offset + (Vector3.up * distanceMultiplier.y);
        transform.LookAt(player.transform.position + Vector3.up * 1f);
    }
}