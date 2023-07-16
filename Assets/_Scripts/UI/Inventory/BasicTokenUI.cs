using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class BasicTokenUI : InventoryItemUI, IRaycastable {
    [SerializeField] Image image;
    [SerializeField] TMP_Text text;

    GameObject itemDisplay;
    
    public override GameObject GetItemDisplay() {
        if (!itemDisplay) return image.gameObject;
        return itemDisplay;
    }

    public override void Initialize(InventoryItem inventoryItem, int amount) {
        base.Initialize(inventoryItem, amount);
        image.sprite = inventoryItem.Image;
        text.text = " x" + amount;
    }

    public void SetItemDisplay(GameObject itemDisplay) {
        this.itemDisplay = itemDisplay;
    }

    public void OnRaycast() {
        OnInventoryItemSelected(this);
    }
}