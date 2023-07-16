using System;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class InventoryItemUI : MonoBehaviour, IPointerUpHandler, IPointerDownHandler {
    protected InventoryItem inventoryItem;
    protected int amount;

    public static event Action<InventoryItemUI> InventoryItemSelected;
    public InventoryItem InventoryItem => inventoryItem;

    public abstract GameObject GetItemDisplay();

    //public string Description { get; set; } = "No description was set";

    public virtual void Initialize(InventoryItem inventoryItem, int amount) {
        this.inventoryItem = inventoryItem;
        this.amount = amount;
    }

    protected void OnInventoryItemSelected(InventoryItemUI item) {
        InventoryItemSelected?.Invoke(item);
    }

    public void OnPointerDown(PointerEventData eventData) {
    }

    public virtual void OnPointerUp(PointerEventData eventData) {
        Debug.Log("UP");
        if (!eventData.dragging) InventoryItemSelected?.Invoke(this);
    }
}