using UnityEngine;

public class DestroyOnFinish : MonoBehaviour {
    [SerializeField] float timeBuffer = 5f;

    void Start() {
        Destroy(gameObject, timeBuffer);
    }
}