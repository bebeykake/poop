using UnityEngine;

public class PlayerSwitchTrigger : MonoBehaviour
{

    //public GameObject particleObject;

    //When the Player ENTERS this trigger collider we...
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("The Player entered");//replace with whatever function you want
            //TurnOnParticlesGO();
        }
    }

    //When the Player STAYS this trigger collider we...
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("The Player stays");//replace with whatever function you want
            
        }
    }

    //When the Player EXITS this trigger collider we...
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("The Player exits");//replace with whatever function you want
        }
    }

    //Example functions to put in here:

    //Play a sound//
    //public AudioClip oneShotClip;

    //public void PlaySFXOnce()
    //{
    //    var src = GetComponent<AudioSource>();
    //    if (src != null && oneShotClip != null)
    //        src.PlayOneShot(oneShotClip);
    //}

    //Play one clip of animation//
    //bool played = false;
    //public void TriggerAnimOnce()
    //{
    //    if (played) return;
    //    played = true;
    //    var anim = GetComponent<Animator>();
    //    if (anim != null) anim.SetTrigger("Play");
    //}

    //PLAY a particle system//
    //public GameObject particleObject;
    //public void TurnOnParticlesGO()
    //{
    //    if (particleObject == null) return;
    //    //particleObject.SetActive(true);
    //    var ps = particleObject.GetComponent<ParticleSystem>();
    //    if (ps != null) ps.Play();
    //}

}
