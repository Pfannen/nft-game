using UnityEngine;

[CreateAssetMenu(fileName = "ProjectileWeapon", menuName = "Macho/Weapon/ProjectileWeapon", order = 0)]
public class ProjectileWeapon : Weapon
{
    [SerializeField] Projectile defaultProjectile;

    public override void Attack(Transform spawn)
    {
        var obj = Instantiate(defaultProjectile, spawn.position, Quaternion.identity, spawn);
        obj.Fire(Range);
    }
}