using UnityEngine;

public class DoorTrigger : MonoBehaviour, IInteractable
{
    public string objectName = "Charging Door"; // Name shown in logs (can customize per object)
    public Animator animator;
    private bool openD = false;

    // Triggered when the player looks at the object
    public void OnLookAt()
    {
        if (!openD)
        {
            if (GameManager.Instance?.coins <= 0)
            {
                PlayerPromptUI.Instance.Show("Not enough coins to open door!", 1f);
            }
            else
            {
                PlayerPromptUI.Instance.Show("Press E to open door!", 1f);
            }
        }
    }

    // Triggered when the player presses the interact button while looking at the object
    public void OnInteract()
    {
       if(GameManager.Instance?.coins >= 1 && !openD)
        {
            openD = true;

            animator.SetTrigger("open");
            
            GameManager.Instance?.RemoveCoin(1);
            gameObject.layer = LayerMask.NameToLayer("Default");

        }
    }

    // Triggered when the player stops looking at the object
    public void OnDisengage()
    {
        Debug.Log("Disengage: " + objectName);
    }
}
