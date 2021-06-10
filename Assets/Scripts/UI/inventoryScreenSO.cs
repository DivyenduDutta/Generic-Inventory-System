using UnityEngine;
using System.Collections.Generic;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using TMPro;
using System;

/// <summary>
/// This handles the inventory screen on the UI
/// It uses ScriptableObjects
/// </summary>
public class inventoryScreenSO : MonoBehaviour
{

    [Serializable]
    private class InventoryItemAssets
    {
        [SerializeField]
        private InventoryItem inventoryItem;
        
        [SerializeField]
        private AssetReference inventoryItemUIAsset;

        public InventoryItem InventoryItem
        {
            get { return inventoryItem; }
        }

        public AssetReference InventoryItemUIAsset
        {
            get { return inventoryItemUIAsset; }

        }
    }


    private GameObject inventoryInstruction;

    [SerializeField]
    private GameObject inventoryWindow;

    [SerializeField]
    private List<InventoryItemAssets> inventoryItemAssets;
    
    private Dictionary<InventoryItem, AssetReference> inventoryUIItems;

    [SerializeField]
    private List<GameObject> inventoryUISlots;

    [SerializeField]
    private Inventory InventorySO; //instead of singleton, we directly associate the SO asset from the editor

    //this list stores the instantiated assets which we'll release after their use is
    //complete, which in our case is after we close the inventory screen
    private List<GameObject> instantiatedInventoryItemAssets;

    //we also need to release the asset operation handles
    private List<AsyncOperationHandle> inventoryItemUIAssetReferences;

    private void Awake()
    {
        //populating the dictionary from the List because Unity
        //does not serialize dictionaries and this seemed easier
        //for now rather than writing custom serializers/deserializers
        inventoryUIItems = new Dictionary<InventoryItem, AssetReference>();

        instantiatedInventoryItemAssets = new List<GameObject>();
        inventoryItemUIAssetReferences = new List<AsyncOperationHandle>();
    }

    private void Start()
    {
        inventoryInstruction = GameObject.FindGameObjectWithTag("InventoryInstruction");
        InitializeInventoryUIItemsDictionary();
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            //open inventory with i
            inventoryInstruction.SetActive(false);
            inventoryWindow.SetActive(true);
            
            PopulateInventoryScreen();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //close inventory with ESC
            inventoryInstruction.SetActive(true);
            inventoryWindow.SetActive(false);

            ReleaseLoadedInventoryUIAssets();
        }
    }

    private void InitializeInventoryUIItemsDictionary()
    {
        foreach (InventoryItemAssets invAsset in inventoryItemAssets)
        {
            inventoryUIItems[invAsset.InventoryItem] = invAsset.InventoryItemUIAsset;
        }
    }

    private void PopulateInventoryScreen()
    {
        int itemCounter = 0;
        
        //using SO to remove the hard dependancy of using singletons here
        foreach (Inventory.InventoryItemAndAmt item in InventorySO.InventoryItems)
        {
            if ((itemCounter+1) > inventoryUISlots.Count)
            {
                Debug.LogError("Too many unique items in inventory to fit on the UI");
                return;
            }
            GameObject inventoryUISlot = inventoryUISlots[itemCounter++];

            PopulateInventoryItemImage(inventoryUISlot, item.InventoryItem);

            PopulateInventoryItemAmount(inventoryUISlot, item.Amount.ToString());
        }
    }

    private void PopulateInventoryItemImage(GameObject inventoryUISlot, 
        InventoryItem inventoryItem)
    {
        AssetReference inventoryAssetRef = inventoryUIItems[inventoryItem];
        if (!inventoryAssetRef.RuntimeKeyIsValid())
        {
            Debug.LogError("$Invalid key {inventoryAsset.RuntimeKey.ToString()}");
            return;
        }

        AsyncOperationHandle<GameObject> opHandle = Addressables.LoadAssetAsync<GameObject>(inventoryAssetRef);

        //save the asset references in a list so we can release them later on
        inventoryItemUIAssetReferences.Add(opHandle);
        
        opHandle.Completed += (operation) =>
        {
            //Debug.LogFormat("positions are: {0}", inventoryUISlot.GetComponent<RectTransform>().position.ToString());
            inventoryAssetRef.
                InstantiateAsync(new Vector3(0,0,0), Quaternion.identity).Completed += (operationHandle) =>
            {
                GameObject inventoryUIItem = operationHandle.Result;
                
                //save the instantiated assets in a list so we can release them later on
                instantiatedInventoryItemAssets.Add(inventoryUIItem);
                
                inventoryUIItem.GetComponent<RectTransform>().SetParent(inventoryUISlot.GetComponent<RectTransform>(), false);
            };
        };
    }

    private void ReleaseLoadedInventoryUIAssets()
    {
        for (int i=0; i<instantiatedInventoryItemAssets.Count; ++i)
        {
            Addressables.ReleaseInstance(instantiatedInventoryItemAssets[i]);
        }
        instantiatedInventoryItemAssets.Clear();

        for (int i=0; i<inventoryItemUIAssetReferences.Count; ++i)
        {
            Addressables.Release(inventoryItemUIAssetReferences[i]);
        }
        inventoryItemUIAssetReferences.Clear();
    }

    private void PopulateInventoryItemAmount(GameObject inventoryUISlot, string amount)
    {
        inventoryUISlot.GetComponentInChildren<TextMeshProUGUI>().text = amount;
    }
}
