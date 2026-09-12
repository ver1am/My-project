using UnityEngine;
using UnityEngine.InputSystem;

public class CameraLook : MonoBehaviour
{
    [Header("Sensivity")]
    public float mouseSensivity = 15f;

    private float xrot = 0f;
    private float yrot = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (Mouse.current == null) return;

        Vector2 mousedelta = Mouse.current.delta.ReadValue();

        float mouseX = mousedelta.x * mouseSensivity * Time.deltaTime;
        float mouseY = mousedelta.y * mouseSensivity * Time.deltaTime;

        xrot -= mouseY; // В юнити y rotation это по оси x

        yrot += mouseX;

        transform.localRotation = Quaternion.Euler(xrot,yrot,0f);
    }
}
