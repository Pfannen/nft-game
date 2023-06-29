using UnityEngine;
using Web3Helpers;

[System.Serializable]
public class EquippableItem : InventoryItem {
    [SerializeField] protected EquipmentType equipmentType;

    public override bool UseTooltipButton => true;

    #region These methods should only be used when creating an instance at runtime
    
    public void EquipItem(EquipmentManager manager) {
        manager.SetEquipment(equipmentType, this);
    }

    public void SetEquipmentType(EquipmentType equipmentType) {
        this.equipmentType = equipmentType;
    }

    #endregion

    public bool IsEquipped(EquipmentManager manager) {
        return manager.GetEquipment(equipmentType) == this;
    }

    protected override void TooltipMethod() {
        EquipItem(Tooltip.EquipmentManager);
    }
}