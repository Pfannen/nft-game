using UnityEngine;

public abstract class AttackManager : MonoBehaviour {
    [SerializeField] private Weapon weapon;
    [SerializeField] private Transform weaponSpawnLocation;

    protected Transform weaponShootTransform { get; private set; }
    protected float timeSinceAttack { get; private set; } = 0f;
    protected Weapon equippedWeapon => weapon;

    protected virtual void Start() {
        SetWeapon(weapon);
    }

    protected virtual void Update() {
        timeSinceAttack += Time.deltaTime;
    }

    protected virtual void Attack() {
        if (timeSinceAttack >= weapon?.Cooldown) {
            weapon?.Attack(weaponShootTransform, (int)transform.localScale.x);
            timeSinceAttack = 0;
        }
    }

    protected void SetWeapon(Weapon weapon) {
        this.weapon = weapon;
        EquipWeapon();
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