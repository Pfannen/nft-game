using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CanvasRaycastManager : MonoBehaviour {
    /* void Update() {
        if (Input.GetMouseButtonDown(0)) ProcessRaycastables();
    } */

    public static List<RaycastResult> Raycast() {
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
        pointerEventData.position = Input.mousePosition;
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerEventData, results);
        return results;
    }

    private void ProcessRaycastables() {
        foreach(var result in CanvasRaycastManager.Raycast()) {
            if (result.gameObject.TryGetComponent<IRaycastable>(out IRaycastable raycastable)) {
                raycastable.OnRaycast();
            }
        }
    }
}