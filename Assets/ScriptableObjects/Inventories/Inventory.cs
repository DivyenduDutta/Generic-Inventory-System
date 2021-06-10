using UnityEngine;
using System;
using System.Collections.Generic;

/// <summary>
/// this class represents the entire inventory which in itself is a list
/// of ScriptableObjects
/// </summary>
[CreateAssetMenu(fileName = "NewInventory", menuName = "New Inventory")]
public class Inventory : ScriptableObject
{
    [Serializable]
    public class InventoryItemAndAmt
    {
        public InventoryItem InventoryItem;
        public int Amount;
    }


    public List<InventoryItemAndAmt> InventoryItems;
}
