using UnityEngine;

public class EnemyAttackManager : AttackManager {
    [SerializeField] Transform target;
    protected override void Update() {
        base.Update();
        if (Mathf.Abs(Vector2.Distance(target.position, transform.position)) <= equippedWeapon.Range) {
            Attack();
        }
    }
}