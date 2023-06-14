using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float speed = 1f;
    [SerializeField] GameObject explosion;
    [SerializeField] LayerMask explosionLayerMask;

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
        Vector2 explosionPosition = transform.position;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(explosionPosition, 5f, explosionLayerMask);
        foreach (var collider in colliders) {
            if (collider.TryGetComponent<Rigidbody2D>(out Rigidbody2D rb))
            {
                Vector2 direction = collider.transform.position - transform.position;
                float distance = direction.magnitude;

                if (distance > 0f)
                {
                    float forceMagnitude = 5f / distance;
                    rb.AddForce(direction.normalized * forceMagnitude, ForceMode2D.Impulse);
                    Debug.Log(direction.normalized * forceMagnitude);
                }
            }
        }
    }
}
