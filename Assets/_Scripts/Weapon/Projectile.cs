using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float speed = 1f;
    [SerializeField] protected float damage = 1f;
    [SerializeField] bool isHoming = false;
    [SerializeField] bool destroyOnCollision = true;
    [SerializeField] float destroyTime = 2f;
    [SerializeField] Vector2 initialImpulse = Vector2.zero;
    [SerializeField] GameObject destroyVfx;

    public void Fire(float range, int dir) {
        var rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(speed * dir * range, 0);
        rb.AddForce(initialImpulse, ForceMode2D.Impulse);
        transform.parent = null;
        Destroy(this.gameObject, destroyTime);
    }

    protected virtual void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.TryGetComponent<HealthManager>(out HealthManager hM)) hM.TakeHealth(damage);
        if (destroyOnCollision) Destroy(gameObject);
    }

    protected virtual void OnDestroy() {
        if (destroyVfx != null) Instantiate(destroyVfx, transform.position, Quaternion.identity);
    }
}
