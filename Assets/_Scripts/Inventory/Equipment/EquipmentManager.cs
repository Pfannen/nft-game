using System;
using System.Collections.Generic;
using UnityEngine;
using Web3Helpers;

public class EquipmentManager : MonoBehaviour {
    protected Dictionary<EquipmentType, EquippableItem> equipment = new Dictionary<EquipmentType, EquippableItem>();

    public event Action EquipmentChanged;

    public void SetEquipment(EquipmentType type, EquippableItem equippable) {
        if (equipment.ContainsKey(type)) equipment[type] = equippable;
        else equipment.Add(type, equippable);
        EquipmentChanged?.Invoke();
    }

    public EquippableItem GetEquipment(EquipmentType type) {
        if (equipment.ContainsKey(type)) return equipment[type];
        else return null;
    }
}