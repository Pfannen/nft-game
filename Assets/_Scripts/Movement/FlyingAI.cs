using UnityEngine;
using Pathfinding;

public class FlyingAI : MonoBehaviour {
    [SerializeField] Transform target;
    [SerializeField] float speed = 200f;
    [SerializeField] float waypointTolerance = 3f;
    [SerializeField] Transform sprites;

    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;

    Seeker seeker;
    Rigidbody2D rb;

    void Start() {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        InvokeRepeating("UpdatePath", 0f, .5f);
    }

    void FixedUpdate() {
        if (path == null) return;
        if (currentWaypoint >= path.vectorPath.Count) {
            reachedEndOfPath = true;
            return;
        } 
        else reachedEndOfPath = false;

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;
        if (Mathf.Sign(sprites.localScale.x) != Mathf.Sign(force.x) && force.x != 0) sprites.localScale = new Vector2(sprites.localScale.x * -1, sprites.localScale.y); 
        rb.AddForce(force);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
        if (distance < waypointTolerance) currentWaypoint++;
    }

    private void OnPathComplete(Path p) {
        if (!p.error) {
            path = p;
            currentWaypoint = 0;
        }
    }

    private void UpdatePath() {
        if (seeker.IsDone()) seeker.StartPath(transform.position, target.position, OnPathComplete);
    }
}