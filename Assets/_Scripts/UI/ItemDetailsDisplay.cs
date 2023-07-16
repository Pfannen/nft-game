using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ItemDetailsDisplay : MonoBehaviour {
    [SerializeField] TMP_Text nameIdText;
    [SerializeField] RectTransform itemDisplay;

    /* public void SetItem(InventoryItem item) {
        itemImage.preserveAspect = true;
        itemImage.sprite = item.Image;
        nameIdText.text = $"{item.ItemName}\nCollection: {item.Collection}\nToken Id: {item.TokenId}";
    } */

    public void SetItem(InventoryItemUI tokenUI) {
        var item = tokenUI.InventoryItem;
        if (itemDisplay.transform.childCount > 0) Destroy(itemDisplay.transform.GetChild(0).gameObject);
        Instantiate(tokenUI.GetItemDisplay(), Vector3.zero, Quaternion.identity).transform.SetParent(itemDisplay, false);
        //itemImage.sprite = item.Image;
        nameIdText.text = $"{item.ItemName}\nCollection: {item.Collection}\nToken Id: {item.TokenId}";
    }
}