using UnityEngine;
using UnityEngine.UI;
using Web3Helpers;

[RequireComponent(typeof(Image))]
public class EquippableSlotUI : InventoryItemUI {
    [SerializeField] CollectionIdentifier collection;
    [SerializeField] int layerOrder;

    Image image;

    void Awake() {
        image = GetComponent<Image>();
        image.preserveAspect = true;
    }

    void OnEnable() {
        SerializableCharacterManager.Instance.ItemWorn += OnItemWorn;
    }

    public void SetCollection(CollectionIdentifier collection) {
        this.collection = collection;
    }

    public void SetLayerOrder(int layerOrder) {
        this.layerOrder = layerOrder;
    }

    public void SetImage(Sprite sprite) {
        image.sprite = sprite;
    }

    private void OnItemWorn(CharacterLayerItem item) {
        if (item.LayerOrder == layerOrder) SetImage(item.Sprite);
        inventoryItem = item;
    }
}