using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CanvasRaycastManager : MonoBehaviour {
    void Update() {
        if (Input.GetMouseButtonDown(0)) Raycast();
    }

    private void Raycast() {
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
        pointerEventData.position = Input.mousePosition;
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerEventData, results);
        foreach(var result in results) {
            if (result.gameObject.TryGetComponent<IRaycastable>(out IRaycastable raycastable)) {
                raycastable.OnRaycast();
            }
        }
    }
}