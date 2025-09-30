using UnityEngine;
using UnityEngine.InputSystem;

// This script detects objects the player looks at, determines if they’re interactable, and triggers relevant interactions.
// It works with the New Input System and assumes interactables implement IInteractable.
public class PlayerRaycaster : MonoBehaviour
{
    public Transform rayOrigin;// Usually CameraRoot
    public float maxDistance = 3f;// How far the Ray reaches
    public LayerMask interactionMask;// What layers can be interacted with

    [HideInInspector]
    public bool canInteract = true;// Can the player interact now? Use this outside to stop interactions.

    private IInteractable currentTarget;// Stores the currently looked-at interactable object.


    //Runs InteractionCheck() every frame only if canInteract is true.
    void Update()
    {
        if (canInteract)
        {
            InteractionCheck();
        }
    }

    //check interaction logic
    private void InteractionCheck()
    {
        Ray ray = new Ray(rayOrigin.position, rayOrigin.forward);// draw the ray, determine its position and length
        RaycastHit hit;// declare what the ray hits

        IInteractable previousTarget = currentTarget;// Swaps from previous to current target when we disengage 

        if (Physics.Raycast(ray, out hit, maxDistance, interactionMask))
        {
            IInteractable interactable = hit.collider.GetComponent<IInteractable>();

            if (interactable != null)
            {
                if (interactable != currentTarget)
                {

                    if (previousTarget != null)
                    {
                        previousTarget.OnDisengage();
                    }

                    currentTarget = interactable;
                    currentTarget.OnLookAt();
                }
                return;
            }
        }
        if (currentTarget != null)
        {
            currentTarget.OnDisengage();
            currentTarget = null;
        }
    }

    // Called when the player presses the interact key (for .5 seconds (as determined by the InputSystem
    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.performed && currentTarget != null)
        {
            currentTarget.OnInteract();
        }
    }

    // Draws a green debug ray in the Scene view to visualize the interaction line.
    void OnDrawGizmos()
    {
        if (!rayOrigin) return;

        Gizmos.color = Color.green;

        // Draw a line showing the direction and length of the cast
        Vector3 start = rayOrigin.position;
        Vector3 end = start + rayOrigin.forward * maxDistance;
        Gizmos.DrawLine(start, end);
    }
}