using System;
using UnityEngine;

public abstract class InventoryItemUI : MonoBehaviour {
    protected InventoryItem inventoryItem;

    public static event Action<InventoryItemUI> InventoryItemSelected;
    public InventoryItem InventoryItem => inventoryItem;

    //public string Description { get; set; } = "No description was set";

    public virtual void Initialize(InventoryItem inventoryItem) {
        this.inventoryItem = inventoryItem;
    }

    protected void OnInventoryItemSelected(InventoryItemUI item) {
        InventoryItemSelected?.Invoke(item);
    }
}