using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
[DisallowMultipleComponent]
public class Interactable_CoinOrb : MonoBehaviour, IInteractable
{
    [Header("Visuals")]
    public GameObject fullOrb;
    public GameObject emptyOrb;

    [Header("Feedback")]
    [SerializeField] private ParticleSystem vfxPrefab;
    [SerializeField] private AudioClip coinSound;

    [Header("Gameplay")]
    public int CoinsToGive = 1;
    public float bounceStrength = 3f;    // fixed spelling
    public float TimeToDestruct = 5f;

    private bool grabbed = false;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        grabbed = false;

        // Ensure initial visual state
        if (fullOrb) fullOrb.SetActive(true);
        if (emptyOrb) emptyOrb.SetActive(false);

    }

    public void OnLookAt()
    {
        if (!grabbed) Debug.Log("Hold E to grab coin");
    }

    public void OnInteract()
    {
        // Guard immediately to avoid multiple awards if Interact fires twice
        if (grabbed) return;
        grabbed = true;

        // Remove from interaction hits (ensure your raycaster mask ignores "Default")
        gameObject.layer = LayerMask.NameToLayer("Default");

        // 1) Grant coins
        GameManager.Instance?.AddCoin(CoinsToGive);

        // 2) Swap visuals
        if (fullOrb) fullOrb.SetActive(false);
        if (emptyOrb) emptyOrb.SetActive(true);

        // 3) Tiny bounce (keeps things tactile while the rigidbody remains)
        if (rb) rb.AddForce(Vector3.up * bounceStrength, ForceMode.Impulse);

        // 4) SFX that won’t be cut off
        if (coinSound) AudioSource.PlayClipAtPoint(coinSound, transform.position, 1f);

        // (5)Spawn VFX (no collision; Stop Action = Destroy on the prefab)
        if (vfxPrefab)
        {
            var vfx = Instantiate(vfxPrefab, transform.position, Quaternion.identity);
            vfx.Play(); // prefab should have Main.StopAction = Destroy
        }

        // 6) Simple timed destruction
        Destroy(gameObject,TimeToDestruct);
    }

    public void OnDisengage()
    {
        if (!grabbed) Debug.Log("Disengaged coin");
    }

}