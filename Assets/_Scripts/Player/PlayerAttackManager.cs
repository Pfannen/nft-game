using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttackManager : AttackManager {
    EquipmentManager manager;

    void Awake() {
        manager = GetComponent<EquipmentManager>();
    }

    protected override void Start() {
        if (manager) SetWeapon(manager.GetEquipment(EquipmentType.Weapon) as Weapon);
        else base.Start();
    }

    void OnEnable() {
        if (manager) manager.EquipmentChanged += OnEquipmentChanged;
    }

    void OnDisable() {
        if (manager) manager.EquipmentChanged -= OnEquipmentChanged;
    }

    void OnFire(InputValue val) {
        Attack();
    }

    private void OnEquipmentChanged() {
        Weapon weapon = manager.GetEquipment(EquipmentType.Weapon) as Weapon;
        if (weapon != equippedWeapon) SetWeapon(weapon);
    }
}