using UnityEngine;

public class WaypointMovementManager : MonoBehaviour {
    [SerializeField] Waypoint waypoint;
    [SerializeField] float distanceTolerance = .1f;

    private EnemyMovement mover;
    private Vector2 curWaypoint;
    private int curWaypointIndex = 0;
    private int waypointDirection = 1;
    bool afterDisable = false;

    void Awake() {
        mover = GetComponent<EnemyMovement>();
    }

    void Start() {
        curWaypoint = waypoint.GetWaypointAtIndex(curWaypointIndex);
        mover.MoveTowardsPosition(curWaypoint);
    }

    void OnEnable() {
        if (afterDisable) {
            mover.StartMoving(curWaypoint.x - transform.position.x);
            Debug.Log("MOVING: " + (curWaypoint.x - transform.position.x));
        }
    }

    void OnDisable() {
        afterDisable = true;
    }

    void Update() {
        ProcessWaypoint();
    }

    private void ProcessWaypoint() {
        if (Mathf.Abs(transform.position.x - curWaypoint.x) < distanceTolerance) {
            curWaypoint = waypoint.GetNextWaypoint(ref curWaypointIndex, ref waypointDirection);
            mover.MoveTowardsPosition(curWaypoint);
        }
    }
}