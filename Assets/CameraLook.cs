using UnityEngine;
using UnityEngine.InputSystem;

public class CameraLook : MonoBehaviour
{
    public Hand handscript;

    [Header("Sensivity")]
    public float mouseSensivity = 15f;
    public float BarrelSpeed = 0.01f;
    
    public float mode = 0; // 0 - Default | 1 - Moving by mouse
    private float xrot = 0f;
    private float yrot = 0f;
    private Rigidbody rb;
    public float pushforce = 0.005f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = transform.parent.GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Gayboard() {
        if (Keyboard.current == null) return;

        if (Keyboard.current.qKey.isPressed) { // LEFT
            rb.AddTorque(new Vector3(0,0,BarrelSpeed),ForceMode.Impulse);
        } else if (Keyboard.current.eKey.isPressed) { // RIGHT
            rb.AddTorque(new Vector3(0,0,-BarrelSpeed),ForceMode.Impulse);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Mouse.current == null) return;

        if (mode == 0) {
            Gayboard();

            Vector2 mousedelta = Mouse.current.delta.ReadValue();

            float mouseX = mousedelta.x * mouseSensivity * Time.deltaTime;
            float mouseY = mousedelta.y * mouseSensivity * Time.deltaTime;

            xrot -= mouseY; // В юнити y rotation это по оси x

            yrot += mouseX;

            transform.localRotation = Quaternion.Euler(xrot,0f,0f);
            transform.parent.transform.Rotate(Vector3.up * mouseX);
        } else {
            if (handscript.handc != null && handscript.handc.activeSelf) {
                Vector3 pushDirection = handscript.hitinfo.point - transform.position;

                pushDirection.Normalize();
                rb.AddForce(pushDirection * pushforce,ForceMode.Impulse);
            }
        }
    }
}
