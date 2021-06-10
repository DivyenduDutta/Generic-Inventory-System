using UnityEngine;

[CreateAssetMenu(fileName = "NewSwordWeapon", menuName = "Inventory/Weapons/Sword")]
public class Sword : Weapon
{
    public bool IsTwoHanded;
    public int SwordStrikeCrit;
}
