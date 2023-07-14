using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class BasicTokenUI : InventoryItemUI, IRaycastable {
    [SerializeField] Image image;
    [SerializeField] TMP_Text text;

    public override void Initialize(InventoryItem inventoryItem, int amount)
    {
        base.Initialize(inventoryItem, amount);
        image.sprite = inventoryItem.Image;
        text.text = " x" + amount;
    }

    public void OnRaycast() {
        OnInventoryItemSelected(this);
    }
}