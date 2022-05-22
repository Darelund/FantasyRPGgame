using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory/Player")]

public class PlayerInventory : ScriptableObject
{
    public List<InventoryItem> playerInventory = new List<InventoryItem>();
}
