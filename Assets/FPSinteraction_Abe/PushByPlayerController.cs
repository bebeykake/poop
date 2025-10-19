using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(CharacterController))] //Ensures player controller component is there
[RequireComponent(typeof(AudioSource))]
public class PushByPlayerController : MonoBehaviour
{
    public float pushForce = 2f; //How hard are we pushing other objects?

    [SerializeField] private AudioClip BumpBook;

    private AudioSource audioSource;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.spatialBlend = 1f;
        audioSource.playOnAwake = false;
    }
    private void PlaySound(AudioClip clip)
    {
        if (!clip || !audioSource) return;

        audioSource.Stop();
        audioSource.clip = clip;
  
        audioSource.Play(); 
    }
    //Function that detects collision. Put whatever logic you want in it.
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody rb = hit.rigidbody; //grab the rigidbody of the colliding object
        if (rb == null || rb.isKinematic) return; //if no rigidbody or rigidbody does not move by physics, skip

        //This logic applies the push force to the other object. 
        //You could also replace this with other functions (activate a game object, make sound, etc)

        //if(hit.gameObject.CompareTag("Book")){

        //take the particlesystem from the book and play it
        //var ps = hit.GetComponentinChildren<ParticleSystem>();
        //ps.Play();

        //swap mesh object
        //2 objects

        Vector3 pushDir = hit.moveDirection;
        pushDir.y = 0f; // keep push horizontal
        rb.AddForce(pushDir * pushForce, ForceMode.Impulse);

        PlaySound(BumpBook);
        //}
    }
}