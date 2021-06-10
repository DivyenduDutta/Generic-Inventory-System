using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This uses the singleton approach
/// </summary>
class InventoryManager : MonoBehaviour
{
    public Dictionary<CommonConstants.INVENTORY_ITEMS, int> Inventory;

    private static InventoryManager _instance;

    public static InventoryManager InventoryInstance
    {
        get
        {
            return _instance;
        }
        private set { }
    }

    private InventoryManager() { }

    private void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        Inventory = new Dictionary<CommonConstants.INVENTORY_ITEMS, int>();
        /*
         * At the very least we need code changes to change the contents of the
         * inventory in case we use this approach
         */
        //InitializeInventory();
        InitializeInventoryForTutorial();
    }

    private void InitializeInventoryForTutorial()
    {
        Inventory.Add(CommonConstants.INVENTORY_ITEMS.HEALTH_POTION, 1);
        Inventory.Add(CommonConstants.INVENTORY_ITEMS.SWORD, 1);
        Inventory.Add(CommonConstants.INVENTORY_ITEMS.SHIELD, 1);
    }

    private void InitializeInventory()
    {
        Inventory.Add(CommonConstants.INVENTORY_ITEMS.HEALTH_POTION, 2);
        Inventory.Add(CommonConstants.INVENTORY_ITEMS.MANA_POTION, 5);
        Inventory.Add(CommonConstants.INVENTORY_ITEMS.STAMINA_POTION, 10);
        Inventory.Add(CommonConstants.INVENTORY_ITEMS.BOW, 1);
        Inventory.Add(CommonConstants.INVENTORY_ITEMS.SWORD, 2);
        Inventory.Add(CommonConstants.INVENTORY_ITEMS.COIN_BAG, 20);
    }

}
