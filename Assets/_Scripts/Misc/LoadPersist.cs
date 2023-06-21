using UnityEngine;

public class LoadPersist : MonoBehaviour {
    void Start() {
        var loadPersists = FindObjectsOfType<LoadPersist>();
        if (loadPersists.Length == 1) DontDestroyOnLoad(gameObject);
        else Destroy(gameObject);
    }
}