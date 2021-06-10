public static class CommonConstants 
{
    /*
     * so here's the problem with enums. Everytime I need to add a new type of inventory item
     * I need to go here and add a new entry and recompile/build code
     */
    public enum INVENTORY_ITEMS
    {
        HEALTH_POTION,
        MANA_POTION,
        STAMINA_POTION,
        BOW,
        SWORD,
        SHIELD,
        COIN_BAG,
        NONE
    }
}
