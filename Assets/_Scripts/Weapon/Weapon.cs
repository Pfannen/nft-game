using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Weapon : MonoBehaviour
{
    [SerializeField] Bullet bullet;
    [SerializeField] Transform bulletSpawn;

    private void OnFire(InputValue val )
    {
        Debug.Log("Fire");
        Instantiate(bullet, bulletSpawn.position, Quaternion.identity, bulletSpawn);
    }
}
