using UnityEngine;
using UnityEngine.InputSystem; // Важно добавить эту строку

public class CameraMovement : MonoBehaviour
{
    public float speed = 5f;

    void Update()
    {
        float moveHorizontal = 0f;
        float moveVertical = 0f;

        // Считываем нажатия клавиш напрямую через новый Input System
        if (Keyboard.current != null)
        {
            if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed) moveHorizontal = -1f;
            if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed) moveHorizontal = 1f;
            if (Keyboard.current.wKey.isPressed || Keyboard.current.upArrowKey.isPressed) moveVertical = 1f;
            if (Keyboard.current.sKey.isPressed || Keyboard.current.downArrowKey.isPressed) moveVertical = -1f;
        }

        Vector3 move = new Vector3(moveHorizontal, 0f, moveVertical);
        transform.Translate(move * speed * Time.deltaTime, Space.World);
    }
}
