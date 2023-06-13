using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float speed = 1f;

    public void Fire(float range) {
        var rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(speed * transform.parent.parent.localScale.x * range, 0);
        rb.AddForce(new Vector2(0, 5f), ForceMode2D.Impulse);
        transform.parent = null;
        Destroy(this.gameObject, 2f);
    }
}
