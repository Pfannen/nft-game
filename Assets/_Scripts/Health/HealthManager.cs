using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour {
    [SerializeField] float health = 100f;

    public float Health => health;

    public void TakeHealth(float amount) {
        health -= amount;
    }

    public void AddHealth(float amount) {
        health += amount;
    }
}