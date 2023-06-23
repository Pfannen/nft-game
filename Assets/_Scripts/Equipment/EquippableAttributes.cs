using UnityEngine;
using Web3Helpers;

public class EquippableAttributes : MonoBehaviour, IEquippable
{
    public Attributes Attributes { get; set; }

    public void Equip(EquipmentManager equipment) {
        equipment.SetEquipment(EquipmentType.Outfit, this);
    }

    public bool IsEquipped(EquipmentManager equipment) {
        return equipment.GetEquipment(EquipmentType.Outfit) == this;
    }

    public object ItemToEquip()
    {
        return Attributes;
    }
}