using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttackManager : MonoBehaviour {
    [SerializeField] Weapon weapon;
    [SerializeField] Transform weaponSpawnLocation;

    Transform weaponShootTransform = null;
    float timeSinceAttack = 0f;
    float curWeaponCooldown = float.MaxValue;

    void Start() {
        SetWeapon(weapon);
    }

    void Update() {
        timeSinceAttack += Time.deltaTime;
    }

    void OnFire(InputValue val) {
        if (timeSinceAttack >= curWeaponCooldown) {
            weapon?.Attack(weaponShootTransform, (int)transform.localScale.x);
            timeSinceAttack = 0;
        }
    }

    public void SetWeapon(Weapon weapon) {
        this.weapon = weapon;
        EquipWeapon();
        curWeaponCooldown = weapon != null ? weapon.Cooldown : float.MaxValue;
    }

    private void EquipWeapon() {
        for (int i = weaponSpawnLocation.childCount - 1; i >= 0; i--) {
            Destroy(weaponSpawnLocation.GetChild(i).gameObject);
        }
        if (weapon != null && weapon.Prefab != null) {
            var weaponTransform = Instantiate(weapon.Prefab, weaponSpawnLocation.position, Quaternion.identity, weaponSpawnLocation).transform;
            weaponShootTransform = weaponTransform;
            for (int i = 0; i < weaponTransform.childCount; i++) {
                var child = weaponTransform.GetChild(i);
                if (child.tag == "Shoot") weaponShootTransform = child;
            }
        } else weaponShootTransform = weaponSpawnLocation;
    }
}