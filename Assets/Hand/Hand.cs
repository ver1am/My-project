using UnityEngine;
using UnityEngine.InputSystem;

public class Hand : MonoBehaviour
{
    public CameraLook look;

    [Header("Hand settings")]
    public Camera mainCamera;
    public float distance = 10f;
    public GameObject hand;
    public GameObject handc;
    public LayerMask hitLayers;
    public RaycastHit hitinfo;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (mainCamera == null) {
            mainCamera = Camera.main;
        }

        if (hand != null && handc != null) {
            hand.SetActive(false);
            handc.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        GameObject HAND; // Hand or HandC

        Ray ray = new Ray(transform.position,transform.forward);

        if (Physics.Raycast(ray,out hitinfo,distance,hitLayers)) {
            // IInteractable
            IInteractable interactable = hitinfo.transform.GetComponent<IInteractable>();

            if (Mouse.current.leftButton.isPressed) {
                HAND = handc;
                if (look.mode != 2) { // 2 Is interacting
                    handc.SetActive(true);
                    hand.SetActive(false);
                    if (interactable != null) {
                        interactable.Interact();
                        look.mode = 2;
                    } else {
                        look.mode = 1;
                    }
                }
            } else {
                HAND = hand;
                hand.SetActive(true);
                handc.SetActive(false);
                look.mode = 0;
            }

            HAND.transform.position = hitinfo.point + (hitinfo.normal * 0.01f);

            HAND.transform.rotation = Quaternion.LookRotation(hitinfo.normal, transform.up);
        } else {
            if (hand.activeSelf || handc.activeSelf) {
                hand.SetActive(false);
                handc.SetActive(false);
                look.mode = 0;
            }        
        }
    }
}