using UnityEngine;
using UnityEngine.InputSystem;

public class Hand : MonoBehaviour
{

    [Header("Distance for hand")]
    public float distance = 10f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray(transform.position,transform.forward);

        RaycastHit hitinfo;

        if (Physics.Raycast(ray,out hitinfo,distance)) {
            print(hitinfo.distance);

            if (Mouse.current.leftButton.isPressed) {
                Destroy(hitinfo.collider.gameObject);
            }
        }
    }
}
