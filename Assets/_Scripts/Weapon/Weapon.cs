using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "Weapon", menuName = "Macho/Weapon", order = 0)]
public abstract class Weapon : ScriptableObject, IEquippable
{
    [SerializeField] GameObject prefab;
    [SerializeField] float damage = 1f;
    [SerializeField] float range = 5f;
    [SerializeField] float cooldown = 1f;
    [SerializeField] AnimationClip[] animations = null;

    public GameObject Prefab => prefab;
    public float Damage => damage;
    public float Range => range;
    public float Cooldown => cooldown;
    public AnimationClip[] Animations => animations;

    public abstract void Attack(Transform spawn, int dir);

    public void Equip(EquipmentManager equipment) {
        throw new NotImplementedException();
    }

    public bool IsEquipped(EquipmentManager equipment) {
        throw new NotImplementedException();
    }

    public object ItemToEquip() {
        throw new NotImplementedException();
    }
}
