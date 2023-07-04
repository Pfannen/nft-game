using UnityEngine;
using Web3Helpers;

[System.Serializable]
public class EquippableItem : InventoryItem {
    [SerializeField] protected EquipmentType equipmentType;

    public override bool IsUsable => true;

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

    protected override void UseItemMethod() {
        EquipItem(Tooltip.EquipmentManager);
    }
}