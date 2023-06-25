using System;
using UnityEngine;

public abstract class InventoryItemUI : MonoBehaviour {
    protected InventoryItem inventoryItem;
    protected int amount;

    public static event Action<InventoryItemUI> InventoryItemSelected;
    public InventoryItem InventoryItem => inventoryItem;

    //public string Description { get; set; } = "No description was set";

    public virtual void Initialize(InventoryItem inventoryItem, int amount) {
        this.inventoryItem = inventoryItem;
        this.amount = amount;
    }

    protected void OnInventoryItemSelected(InventoryItemUI item) {
        InventoryItemSelected?.Invoke(item);
    }
}