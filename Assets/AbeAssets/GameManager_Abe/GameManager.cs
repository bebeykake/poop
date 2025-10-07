using UnityEngine;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    // Globally accessible reference to the single active GameManager.
    // Readable everywhere, but only this class can set it.
    public static GameManager Instance { get; private set; }

    // Simple shared game state (add or modify as needed for the demo).
    public int coins = 0;
    // public float PlayerHp = 100f;   // Example: track player health
    // public bool Poisoned = false;   // Example: status effect
    // Runtime inventory (not visible in the Inspector by default)
    public Dictionary<string, int> inventory = new Dictionary<string, int>();


    private void Awake()
    {
        // If another GameManager already exists and it isn't us, destroy this object.
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // remove the duplicate GameObject, not just the component
            return;
        }

        // Otherwise, set the singleton instance to this object.
        Instance = this;

        // Persist across scene loads.
        DontDestroyOnLoad(gameObject);
    }

    // --- Public Application Programming Interface (API): call these functions instead of changing fields directly.
    // This lets you add SFX/UI hooks without touching other scripts.
    //Add more depending on the variables you want to keep track off (HP, Poisoned, etc)

    // Add coins from anywhere in the scene.
    public void AddCoin(int coinsToAdd)
    {
        if (coinsToAdd == 0) return;
        coins += coinsToAdd;
        if (coins < 0) coins = 0; // clamp to zero
        Debug.Log($"Added {coinsToAdd} coins (total: {coins})");
        // Stretch goal: notify a UI label here.
    }

    // Remove coins from anywhere in the scene.
    public void RemoveCoin(int coinsToRemove)
    {
        if (coinsToRemove == 0) return;
        coins -= coinsToRemove;
        if (coins < 0) coins = 0; // clamp to 0
        Debug.Log($"Removed {coinsToRemove} coins (total: {coins})");
    }

    // ---Inventory (Dictionary) functions

    // Add item(s) to inventory. Negative amount will subtract.
    public void AddItem(string itemId, int amount = 1)
    {
        if (string.IsNullOrEmpty(itemId) || amount == 0) return; // ignore bad calls
        if (!inventory.ContainsKey(itemId)) inventory[itemId] = 0;
        inventory[itemId] += amount;
        if (inventory[itemId] <= 0) inventory.Remove(itemId); // keep inventory clean
    }

    // Check if the inventory has at least N of an item.
    public bool HasItem(string itemId, int atLeast = 1)
    {
        return inventory.TryGetValue(itemId, out var count) && count >= atLeast;
    }

    // Remove item(s) if present; returns true on success.
    public bool ConsumeItem(string itemId, int amount = 1)
    {
        if (!HasItem(itemId, amount)) return false;
        inventory[itemId] -= amount;
        if (inventory[itemId] <= 0) inventory.Remove(itemId);
        return true;
    }

    // Get count of an item (0 if not present).
    public int GetCount(string itemId)
    {
        return inventory.TryGetValue(itemId, out var count) ? count : 0;
    }

}