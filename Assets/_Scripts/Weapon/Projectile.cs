using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float speed = 1f;
    [SerializeField] GameObject explosion;

    public void Fire(float range, int dir) {
        var rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(speed * dir * range, 0);
        rb.AddForce(new Vector2(0, 5f), ForceMode2D.Impulse);
        transform.parent = null;
        Destroy(this.gameObject, 2f);
    }

    void OnDestroy() {
        if (explosion != null) {
            Instantiate(explosion, transform.position, Quaternion.identity);
        }
    }
}
