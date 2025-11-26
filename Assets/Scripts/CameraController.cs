using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [Header("Target (Pivot point)")]
    public Transform target;

    [Header("Orbit Settings")]
    public float orbitSpeed = 200f;
    public float minPitch = 10f;
    public float maxPitch = 80f;

    [Header("Zoom Settings")]
    public float zoomSpeed = 200f;
    public float minZoom = 20f;
    public float maxZoom = 200f;

    [Header("Pan Settings")]
    public float panSpeed = 40f;

    private float yaw = 0f;
    private float pitch = 45f;
    private float distance = 60f;

    [Header("Input Actions")]
    public InputAction moveAction;

    void LateUpdate()
    {
        //if (!target) return;

        HandleOrbit();
        HandleZoom();
        HandlePan();
        HandleKeyboardPan();
        UpdateCameraPosition();
    }

    void HandleOrbit()
    {
        if (Mouse.current.rightButton.isPressed)
        {
            yaw += Input.GetAxis("Mouse X") * orbitSpeed * Time.deltaTime;
            pitch -= Input.GetAxis("Mouse Y") * orbitSpeed * Time.deltaTime;
            pitch = Mathf.Clamp(pitch, minPitch, maxPitch);
        }
    }

    void HandleZoom()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (Mathf.Abs(scroll) > 0.001f)
        {
            distance -= scroll * zoomSpeed * Time.deltaTime;
            distance = Mathf.Clamp(distance, minZoom, maxZoom);
        }
    }

    void HandlePan()
    {
        if (Input.GetMouseButton(2))
        {
            Vector3 right = transform.right;
            Vector3 forward = Vector3.ProjectOnPlane(transform.forward, Vector3.up).normalized;

            Vector3 panDir =
                -right * Input.GetAxis("Mouse X") +
                -forward * Input.GetAxis("Mouse Y");

            target.position += panDir * panSpeed * Time.deltaTime;
        }
    }

    // RTS 스타일로 키보드로도 이동 가능
    void HandleKeyboardPan()
    {
        Vector3 move = Vector3.zero;

        if (Input.GetKey(KeyCode.W)) move += Vector3.forward;
        if (Input.GetKey(KeyCode.S)) move += Vector3.back;
        if (Input.GetKey(KeyCode.A)) move += Vector3.left;
        if (Input.GetKey(KeyCode.D)) move += Vector3.right;
        if (Input.GetKey(KeyCode.Q)) move += Vector3.down;
        if (Input.GetKey(KeyCode.E)) move += Vector3.up;

        if (move != Vector3.zero)
        {
            Vector3 worldMove = transform.TransformDirection(move);
            worldMove.y = 0f; // 평면 이동

            target.position += worldMove * panSpeed * Time.deltaTime;
        }
    }

    void UpdateCameraPosition()
    {
        Quaternion rot = Quaternion.Euler(pitch, yaw, 0f);
        Vector3 offset = rot * new Vector3(0f, 0f, -distance);

        transform.position = target.position + offset + Vector3.up * 5f;
        transform.LookAt(target.position);
    }
}
