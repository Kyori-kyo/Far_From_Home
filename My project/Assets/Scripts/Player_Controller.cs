using UnityEngine.EventSystems;
using UnityEngine;

public class Player_Controller : MonoBehaviour
{
    public Interactable focus;

    public LayerMask movementMask;

    Camera cam;
    playerMotor motor;

    // Start is called before the first frame update
    void Start() {

        cam = Camera.main;
        motor = GetComponent<playerMotor>();
    }

    // Update is called once per frame
    void Update() {

        if (EventSystem.current.IsPointerOverGameObject()) {

            return;
        }

        if (Input.GetMouseButtonDown(1)) {

            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100, movementMask)) {
                Debug.Log("We hit " + hit.collider.name + " " + hit.point);

                // Move or player to what we hit
                motor.MoveToPoint(hit.point);

                // Stop focusing any objects
                RemoveFocus();
            }
        }

        if (Input.GetMouseButtonDown(0)) {

            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100)) {
                Debug.Log("We hit " + hit.collider.name + " " + hit.point);

                // Check if we hit an interactable
                Interactable interactable = hit.collider.GetComponent<Interactable>();
                // If we did, set it as our focus
                if (interactable != null)
                {
                    SetFocus(interactable);
                }
            }
        }
    }

    void SetFocus(Interactable newFocus) {

        if (newFocus != focus)
        {
            if (focus != null) {
                
            focus.OnDefocused();
            }

            focus = newFocus;
            motor.FollowTarget(newFocus);
        }

        newFocus.OnFocused(transform);
    }

    void RemoveFocus() {

        if (focus != null) {
                
            focus.OnDefocused();
        }

        focus = null;
        motor.StopFollowingTarget();
    }
}
