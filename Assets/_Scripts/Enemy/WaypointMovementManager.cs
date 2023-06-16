using UnityEngine;

public class WaypointMovementManager : MonoBehaviour {
    [SerializeField] Waypoint waypoint;

    private EnemyMovement mover;
    private Vector2 curWaypoint;
    private int curWaypointIndex = 0;
    private int waypointDirection = 1;

    void Start() {
        mover = GetComponent<EnemyMovement>();
        curWaypoint = waypoint.GetWaypointAtIndex(curWaypointIndex);
        if (ShouldSwapDirection()) mover.SwapMoveSpeed();
    }

    void Update() {
        ProcessWaypoint();
    }

    private void ProcessWaypoint() {
        if (Mathf.Abs(transform.position.x - curWaypoint.x) < .1) {
            curWaypoint = waypoint.GetNextWaypoint(ref curWaypointIndex, ref waypointDirection);
            if (ShouldSwapDirection()) mover.SwapMoveSpeed();
        }
    }

    private bool ShouldSwapDirection() {
        return mover.GetVelocityDirection() != Mathf.Sign(curWaypoint.x - transform.position.x);
    }
}