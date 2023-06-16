using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour {
    public List<Vector2> waypoints = new List<Vector2>();

    void Awake() {
        for (int i = 0; i < transform.childCount; i++) {
            waypoints.Add(transform.GetChild(i).transform.position);
        }
    }

    public Vector2 GetNextWaypoint(ref int waypointIndex, ref int direction) {
        if (waypointIndex == 0) direction = 1;
        else if (waypointIndex == waypoints.Count - 1) direction = -1;
        waypointIndex += direction;
        return GetWaypointAtIndex(waypointIndex);
    }

    public Vector2 GetWaypointAtIndex(int waypointIndex) {
        return waypoints[Mathf.Clamp(waypointIndex, 0, waypoints.Count - 1)];
    }
}