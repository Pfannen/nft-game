using UnityEngine;

public class Levitate : MonoBehaviour {
    [SerializeField] float speed = 1f;
    [SerializeField] float distance = 3f;

    float curDistance = 0f;
    bool up = true;

    void Update() {
        float move = speed * Time.deltaTime;
        if (up) {
            curDistance += move;
            transform.Translate(0, move, 0);
            if (curDistance > distance) up = false;
        } else {
            curDistance -= move;
            transform.Translate(0, -move, 0);
            if (curDistance < 0) up = true;
        }
    }
}