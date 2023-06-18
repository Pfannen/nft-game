using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttackManager : AttackManager {
    void OnFire(InputValue val) {
        Attack();
    }
}