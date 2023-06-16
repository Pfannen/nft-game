using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour {
    public List<Vector2> Waypoints { get; private set; } = new List<Vector2>();
    public Vector2 CurrentWaypoint { get; private set; }
    private int direction = 1;
    private int curWaypointIndex = 0;

    void Awake() {
        for (int i = 0; i < transform.childCount; i++) {
            Waypoints.Add(transform.GetChild(i).transform.position);
        }
        CurrentWaypoint = Waypoints[curWaypointIndex];
    }

    public void GetNextWaypoint() {
        if (curWaypointIndex == 0) direction = 1;
        else if (curWaypointIndex == Waypoints.Count - 1) direction = -1;
        curWaypointIndex += direction;
        CurrentWaypoint = Waypoints[curWaypointIndex];
    }
}