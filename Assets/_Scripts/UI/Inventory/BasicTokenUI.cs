using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BasicTokenUI : InventoryItemUI, IRaycastable {
    [SerializeField] Image image;
    [SerializeField] TMP_Text text;
    private int tokenId = -1;

    public void Initialize(int tokenId, Sprite image, string text) {
        this.tokenId = tokenId;
        SetImage(image);
        SetText(text);
    }

    public void SetImage(Sprite image) {
        this.image.sprite = image;
    }

    public void SetText(string text) {
        this.text.text = text;
    }

    public void OnRaycast() {
        OnInventoryItemSelected(this);
        Debug.Log($"Token id {tokenId} was selected");
    }
}