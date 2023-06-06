using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float speed = 1f;
    Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(speed * transform.localScale.x, 0);
        rb.AddForce(new Vector2(0, 5f), ForceMode2D.Impulse);
        transform.parent = null;
        Destroy(this.gameObject, 2f);
    }
}
