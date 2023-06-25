using UnityEngine;
using Web3Helpers;

[System.Serializable]
public class EquippableItem : InventoryItem {
    [SerializeField] protected EquipmentType equipmentType;

    public void SetEquipmentType(EquipmentType equipmentType) {
        this.equipmentType = equipmentType;
    }

    public void EquipItem(EquipmentManager manager) {
        manager.SetEquipment(equipmentType, this);
        Debug.Log(JsonUtility.ToJson(new Attributes()));
    }

    public bool IsEquipped(EquipmentManager manager) {
        return manager.GetEquipment(equipmentType) == this;
    }
}