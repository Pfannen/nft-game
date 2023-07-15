using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableTokenUI : BasicTokenUI, IBeginDragHandler, IEndDragHandler, IDragHandler {
    [SerializeField] RectTransform parent;
    [SerializeField] RectTransform content;

    RectTransform rectTransform;
    CanvasGroup canvasGroup;
    Canvas canvas;

    public RectTransform Content => content;

    void Awake() {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponentInChildren<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData) {
        if (canvas == null) canvas = GetComponentInParent<Canvas>();
        content.SetParent(canvas.transform, true);
        canvasGroup.alpha = 0.6f;
    }

    public void OnDrag(PointerEventData eventData) {
        content.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData) {
        var raycasts = CanvasRaycastManager.Raycast();
        foreach(var result in raycasts) {
            if (result.gameObject.TryGetComponent<EquippablesManagerUI>(out EquippablesManagerUI manager)) {
                Debug.Log(manager);
                manager.EquipItem(inventoryItem);
                break;
            }
        }
        canvasGroup.alpha = 1f;
        content.SetParent(parent.transform, true);
        content.anchoredPosition = new Vector2(0,0);
    }
}