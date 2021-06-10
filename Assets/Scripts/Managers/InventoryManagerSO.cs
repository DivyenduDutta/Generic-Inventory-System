using UnityEngine;

/// <summary>
/// This uses Scriptable Objects
/// </summary>
public class InventoryManagerSO : MonoBehaviour
{
    public Inventory Inventory;

    private void Start()
    {
        PrintAllInventoryItems();
    }

    private void PrintAllInventoryItems()
    {
        Debug.Log("You have the following in your inventory...");
        foreach (Inventory.InventoryItemAndAmt item in Inventory.InventoryItems)
        {
            Debug.LogFormat("{0} of {1}", item.InventoryItem.ToString(), item.Amount.ToString());
        }
    }
}
