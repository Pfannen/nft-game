using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ItemDetailsDisplay : MonoBehaviour {
    [SerializeField] TMP_Text nameIdText;
    [SerializeField] Image itemImage;
    [SerializeField] RectTransform test;

    /* public void SetItem(InventoryItem item) {
        itemImage.preserveAspect = true;
        itemImage.sprite = item.Image;
        nameIdText.text = $"{item.ItemName}\nCollection: {item.Collection}\nToken Id: {item.TokenId}";
    } */

    public void SetItem(InventoryItemUI tokenUI) {
        var item = tokenUI.InventoryItem;
        itemImage.preserveAspect = true;
        if (test.transform.childCount > 0) Destroy(test.transform.GetChild(0).gameObject);
        Instantiate(tokenUI.GetItemDisplay(), Vector3.zero, Quaternion.identity).transform.SetParent(test, false);
        //itemImage.sprite = item.Image;
        nameIdText.text = $"{item.ItemName}\nCollection: {item.Collection}\nToken Id: {item.TokenId}";
    }
}