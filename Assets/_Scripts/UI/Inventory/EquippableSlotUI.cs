using UnityEngine;
using UnityEngine.UI;
using Web3Helpers;

[RequireComponent(typeof(Image))]
public class EquippableSlotUI : InventoryItemUI {
    [SerializeField] CollectionIdentifier collection;
    [SerializeField] int layerOrder;
    [SerializeField] Image itemImage;

    Image image;
    GameObject itemDisplay;
    CharacterLayerManager manager;

    public override GameObject GetItemDisplay() {
        if (!itemDisplay) return image.gameObject;
        else return itemDisplay;
    }

    void Awake() {
        image = GetComponent<Image>();
        image.preserveAspect = true;
        itemImage.preserveAspect = true;
    }

    void Start() {
        manager = SerializableCharacterManager.Instance;
        manager.ItemWorn += ProcessLayer;
        manager.ItemRemoved += ProcessLayer;
        ProcessLayer(layerOrder);
    }

    void OnDisable() {
        manager.ItemWorn -= ProcessLayer;
    }

    public void SetCollection(CollectionIdentifier collection) {
        this.collection = collection;
    }

    public void SetLayerOrder(int layerOrder) {
        this.layerOrder = layerOrder;
    }

    private void ProcessLayer(int layerOrder) {
        if (layerOrder != this.layerOrder) return;
        var item = manager.GetItem(layerOrder);
        if (!item) itemImage.sprite = null;
        else itemImage.sprite = item.Sprite;
        inventoryItem = item;
    }
}