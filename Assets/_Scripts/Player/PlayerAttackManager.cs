using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttackManager : MonoBehaviour {
    [SerializeField] Weapon weapon;
    [SerializeField] Transform weaponSpawnLocation;

    void Start() {
        EquipWeapon();
    }

    void OnFire(InputValue val) {
        weapon?.Attack(weaponSpawnLocation);
    }

    public void SetWeapon(Weapon weapon) {
        this.weapon = weapon;
        EquipWeapon();
    }

    private void EquipWeapon() {
        for (int i = weaponSpawnLocation.childCount - 1; i >= 0; i--) {
            Destroy(weaponSpawnLocation.GetChild(i).gameObject);
        }
        if (weapon != null && weapon.Prefab != null) {
            Instantiate(weapon.Prefab, weaponSpawnLocation.position, Quaternion.identity, weaponSpawnLocation);
        }
    }
}