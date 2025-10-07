using UnityEngine;
using TMPro;

public class Simple_coinCounterText : MonoBehaviour
{

    private TMP_Text label;
    private int lastCoins = -1;

    void Awake()
    {
        label = GetComponent<TMP_Text>();
    }

    void Update()
    {
        var gm = GameManager.Instance;
        if (gm == null) return;

        int coins = gm.coins;
        if (coins != lastCoins)
        {
            lastCoins = coins;
            label.text = $"Coins: {coins}";
        }
    }
}
