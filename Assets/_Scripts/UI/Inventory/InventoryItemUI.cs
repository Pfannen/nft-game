using System;
using UnityEngine;

public abstract class InventoryItemUI : MonoBehaviour {
    public static event Action<InventoryItemUI> InventoryItemSelected;

    public string Description { get; set; } = "No description was set";

    protected void OnInventoryItemSelected(InventoryItemUI item) {
        InventoryItemSelected?.Invoke(item);
    }
}