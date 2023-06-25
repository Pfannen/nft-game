using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BasicTokenUI : InventoryItemUI, IRaycastable {
    [SerializeField] Image image;
    [SerializeField] TMP_Text text;

    public override void Initialize(InventoryItem inventoryItem, int amount)
    {
        base.Initialize(inventoryItem, amount);
        image.sprite = inventoryItem.Image;
        text.text = " x" + amount;
    }

    /* public void Initialize(int tokenId, Sprite image, string text) {
        this.tokenId = tokenId;
        SetImage(image);
        SetText(text);
        Description = $"This is token {tokenId}";
    }

    public void SetImage(Sprite image) {
        this.image.sprite = image;
    }

    public void SetText(string text) {
        this.text.text = text;
    } */

    public void OnRaycast() {
        OnInventoryItemSelected(this);
    }
}