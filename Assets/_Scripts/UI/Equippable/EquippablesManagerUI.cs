using UnityEngine;

public class EquippablesManagerUI : MonoBehaviour {
    [SerializeField] WearableManagerUI wearableManager;
    [SerializeField] EquippableItemManager equippableManager;

    public void EquipItem(InventoryItem item) {
        if (item is EquippableItem equippable) {
            equippableManager.EquipItem(equippable);
        } else if (item is CharacterPreset preset) {
            wearableManager.EquipCharacterPreset(preset);
        }
    }
}