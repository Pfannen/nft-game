using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyMovement : MonoBehaviour {
    [SerializeField] float moveSpeed = 1f;
    [SerializeField] Waypoint waypoint;

    Rigidbody2D rb;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity += new Vector2(0, 5f);
        if (Mathf.Sign(moveSpeed) != Mathf.Sign(waypoint.CurrentWaypoint.x - transform.position.x)) {
            moveSpeed *= -1;
            SwapLocalScale();
        }
        rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
        Debug.Log(waypoint.CurrentWaypoint.x);
    }

    void Update() {
        rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
        if (Mathf.Abs(transform.position.x - waypoint.CurrentWaypoint.x) < .1) {
            if (waypoint.CurrentWaypoint.y - transform.position.y > .1) {
                rb.velocity += new Vector2(0, 5f);
            }
            waypoint.GetNextWaypoint();
            if (Mathf.Sign(moveSpeed) != Mathf.Sign(waypoint.CurrentWaypoint.x - transform.position.x)) {
                moveSpeed *= -1;
                SwapLocalScale();
            }
            rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
            Debug.Log(waypoint.CurrentWaypoint.x);
        }
    }

    private void SwapLocalScale() {
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }

    /* void OnTriggerExit2D(Collider2D other) {
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        rb.velocity = new Vector2(rb.velocity.x * -1, 0);
    } */
}