using UnityEngine;

public class ToggleActive : MonoBehaviour {
    [SerializeField] GameObject obj;
    [SerializeField] KeyCode key;

    void Update() {
        if (Input.GetKeyDown(key)) obj.SetActive(!obj.activeSelf);
    }
}