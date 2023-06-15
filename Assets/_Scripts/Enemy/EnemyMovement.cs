using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyMovement : MonoBehaviour {
    [SerializeField] float moveSpeed = 1f;

    Rigidbody2D rb;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(moveSpeed, 0);
    }

    void OnTriggerExit2D(Collider2D other) {
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        rb.velocity = new Vector2(rb.velocity.x * -1, 0);
    }
}