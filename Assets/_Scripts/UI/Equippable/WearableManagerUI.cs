using UnityEngine;
using Web3Helpers;

public class WearableManagerUI : MonoBehaviour {
    [SerializeField] EquippableSlotUI slot;

    void Start() {
        if (transform.childCount == 0) {
            CollectionIdentifier collection = SerializableCharacterManager.Instance.WearableCollection;
            int numLayers = LayerHelper.NumLayers(collection);
            for (int i = 0; i < numLayers; i++) {
                var itemSlot = Instantiate(slot, Vector3.zero, Quaternion.identity, this.transform);
                itemSlot.SetLayerOrder(i);
            }
        }
    }

    public void EquipCharacterPreset(CharacterPreset preset) {
        SerializableCharacterManager.Instance.WearOutfit(preset);
    }
}