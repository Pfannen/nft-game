using System;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour {
    protected Dictionary<EquipmentType, object> equipment = new Dictionary<EquipmentType, object>();

    public event Action EquipmentChanged;

    public void SetEquipment(EquipmentType type, IEquippable equippable) {
        if (equipment.ContainsKey(type)) equipment[type] = equippable.ItemToEquip();
        else equipment.Add(type, equippable.ItemToEquip());
        EquipmentChanged?.Invoke();
    }

    public object GetEquipment(EquipmentType type) {
        if (equipment.ContainsKey(type)) return equipment[type];
        else return null;
    }
}

[System.Serializable]
public enum EquipmentType {
    Pendant,
    Misc,
    Weapon,
    Mount,
    Outfit
}