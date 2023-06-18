using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyMovement : MonoBehaviour {
    [SerializeField] float moveSpeed = 1f;
    [SerializeField] float jumpRaycastDenominator = 1f;
    [SerializeField] LayerMask jumpLayerMask;
    [SerializeField] BoxCollider2D feetCollider;

    Vector2 baseVelocity;
    Rigidbody2D rb;
    private float timeSinceJump = 0;

    void Awake() {
        rb = GetComponent<Rigidbody2D>();
        baseVelocity = new Vector2(moveSpeed, 0);
    }

    void Update() {
        baseVelocity.y = rb.velocity.y;
        rb.velocity = baseVelocity;
        DetectObstacles();
    }

    private void DetectObstacles() {
        timeSinceJump += Time.deltaTime;
        var hit = Physics2D.Raycast(transform.position, Vector2.right * transform.localScale.x, moveSpeed / jumpRaycastDenominator, jumpLayerMask);
        if (hit.collider != null) Jump();
    }

    private void Jump() {
        if (timeSinceJump < .1f) return;
        var hit = Physics2D.Raycast(feetCollider.bounds.center, Vector2.down, (feetCollider.bounds.size.y / 2) + .03f, jumpLayerMask);
        if (hit.collider != null) {
            AddVelocity(new Vector2(0, 5f));
            timeSinceJump = 0;
        }
    }

    public void SwapMoveSpeed() {
        baseVelocity.x *= -1;
        SwapLocalScale();
    }

    public int GetVelocityDirection() {
        return (int)Mathf.Sign(baseVelocity.x);
    }

    public void AddVelocity(Vector2 addVelocity) {
        rb.velocity += addVelocity;
    }

    private void SwapLocalScale() {
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }

    /* void OnTriggerExit2D(Collider2D other) {
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        rb.velocity = new Vector2(rb.velocity.x * -1, 0);
    } */

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.right * moveSpeed / jumpRaycastDenominator * transform.localScale.x);
        Gizmos.DrawLine(feetCollider.bounds.center, feetCollider.bounds.center + Vector3.down * ((feetCollider.bounds.size.y / 2) + .03f));
    } 
}