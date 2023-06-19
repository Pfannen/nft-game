using UnityEngine;

public class TargetChaser : MonoBehaviour {
    [SerializeField] Transform target;
    [SerializeField] float chaseDistance = 10f;
    [SerializeField] float targetDistanceTolerance = 1f;
    [SerializeField] float waitAfterOutOfRange = 1f;

    EnemyMovement mover;
    WaypointMovementManager wMM;
    float timeSinceChase;

    void Start() {
        mover = GetComponent<EnemyMovement>();
        wMM = GetComponent<WaypointMovementManager>();
        timeSinceChase = waitAfterOutOfRange;
        InvokeRepeating("LogIsChasing", 0, 1);
    }

    void Update() {
        float distance = Vector2.Distance(target.transform.position, transform.position);
        if (distance <= chaseDistance) ChaseTarget(distance);
        else {
            timeSinceChase += Time.deltaTime;
            if (timeSinceChase > waitAfterOutOfRange) SetWaypointManagerActive(true);
            else mover.StopMoving();
        }
    }

    void ChaseTarget(float distance) {
        SetWaypointManagerActive(false);
        timeSinceChase = 0;
        if (distance <= targetDistanceTolerance) mover.StopMoving();
        else mover.MoveTowardsPosition(target.position);
    }

    void SetWaypointManagerActive(bool active) {
        if (wMM != null) wMM.enabled = active;
    }

    void LogIsChasing() {
        Debug.Log(wMM.enabled ? "Not chasing" : "Chasing");
    }
}