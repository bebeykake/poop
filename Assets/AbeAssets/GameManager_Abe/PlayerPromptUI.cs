using UnityEngine;
using TMPro;
using System.Collections;

[RequireComponent(typeof(TMP_Text))]
public class PlayerPromptUI : MonoBehaviour
{
    public static PlayerPromptUI Instance { get; private set; }

    [SerializeField] private TMP_Text label;
    [SerializeField] private float defaultDuration = 0f; // 0 = stay until Hide()

    private Coroutine hideCo;

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;

        if (!label) label = GetComponent<TMP_Text>();
        Hide(); // start hidden
    }

    public void Show(string message, float duration = -1f)
    {
        if (hideCo != null) { StopCoroutine(hideCo); hideCo = null; }
        label.text = message;
        label.gameObject.SetActive(true);

        float d = (duration >= 0f) ? duration : defaultDuration;
        if (d > 0f) hideCo = StartCoroutine(HideAfter(d));
    }

    public void Hide()
    {
        if (hideCo != null) { StopCoroutine(hideCo); hideCo = null; }
        label.gameObject.SetActive(false);
    }

    private IEnumerator HideAfter(float t)
    {
        yield return new WaitForSeconds(t);
        Hide();
    }
}