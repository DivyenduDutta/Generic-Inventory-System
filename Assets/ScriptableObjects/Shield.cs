using UnityEngine;

[CreateAssetMenu(fileName = "NewShield", menuName = "Inventory/Shields/AllShield")]
public class Shield : InventoryItem
{
    public int AmountOfDamageBlockedToHealth;
    public int AmountOfDamageBlockedToMana;
    public int AmountOfDamageBlockedToStamina;

}
