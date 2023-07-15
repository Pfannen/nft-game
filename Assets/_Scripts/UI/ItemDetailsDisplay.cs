using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ItemDetailsDisplay : MonoBehaviour {
    [SerializeField] TMP_Text nameIdText;
    [SerializeField] Image itemImage;

    public void SetItem(InventoryItem item) {
        itemImage.preserveAspect = true;
        itemImage.sprite = item.Image;
        nameIdText.text = $"{item.ItemName}\nCollection: {item.Collection}\nToken Id: {item.TokenId}";
    }
}