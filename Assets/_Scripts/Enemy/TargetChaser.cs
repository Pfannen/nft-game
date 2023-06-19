using UnityEngine;

public class TargetChaser : MonoBehaviour {
    [SerializeField] Transform target;
    [SerializeField] float chaseDistance = 10f;
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
        if (Vector2.Distance(target.transform.position, transform.position) < chaseDistance) ChaseTarget();
        else {
            timeSinceChase += Time.deltaTime;
            if (timeSinceChase > waitAfterOutOfRange) SetWaypointManagerActive(true);
            else mover.StopMoving();
        }
    }

    void ChaseTarget() {
        SetWaypointManagerActive(false);
        timeSinceChase = 0;
        mover.MoveTowardsPosition(target.position);
    }

    void SetWaypointManagerActive(bool active) {
        if (wMM != null) wMM.enabled = active;
    }

    void LogIsChasing() {
        Debug.Log(wMM.enabled ? "Not chasing" : "Chasing");
    }
}