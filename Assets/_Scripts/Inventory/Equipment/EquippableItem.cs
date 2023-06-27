using UnityEngine;
using Web3Helpers;

[System.Serializable]
public class EquippableItem : InventoryItem {
    [SerializeField] protected EquipmentType equipmentType;

    public override bool UseTooltipButton => true;

    public void SetEquipmentType(EquipmentType equipmentType) {
        this.equipmentType = equipmentType;
    }

    public void EquipItem(EquipmentManager manager) {
        manager.SetEquipment(equipmentType, this);
    }

    public bool IsEquipped(EquipmentManager manager) {
        return manager.GetEquipment(equipmentType) == this;
    }
}