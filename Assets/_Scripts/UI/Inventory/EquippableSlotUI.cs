using UnityEngine;
using UnityEngine.UI;
using Web3Helpers;

[RequireComponent(typeof(Image))]
public class EquippableSlotUI : MonoBehaviour {
    [SerializeField] CollectionIdentifier collection;
    [SerializeField] int layerOrder;

    Image image;

    void Awake() {
        image = GetComponent<Image>();
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
}