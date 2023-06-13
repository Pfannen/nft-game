using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttackManager : MonoBehaviour {
    [SerializeField] Weapon weapon;
    [SerializeField] Transform spawn;

    void OnFire(InputValue val) {
        weapon?.Attack(spawn);
    }
}