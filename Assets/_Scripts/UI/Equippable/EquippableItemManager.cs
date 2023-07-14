using UnityEngine;

public class EquippableItemManager : MonoBehaviour {
    public void EquipItem(EquippableItem item) {
        SerializableEquipment.Instance.SetEquipment(item.EquipmentType, item);
    }
}