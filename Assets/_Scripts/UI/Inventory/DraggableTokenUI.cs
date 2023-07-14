using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableTokenUI : BasicTokenUI, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler {
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

        public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 0.6f;
    }

    public void OnDrag(PointerEventData eventData)
    {
        content.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        var raycasts = CanvasRaycastManager.Raycast();
        foreach(var result in raycasts) {
            if (result.gameObject.TryGetComponent<WearableManagerUI>(out WearableManagerUI manager)) {
                Debug.Log(manager);
                if (inventoryItem is CharacterPreset preset) manager.EquipCharacterPreset(preset);
                break;
            }
        }
        canvasGroup.alpha = 1f;
        content.SetParent(parent.transform, true);
        content.anchoredPosition = new Vector2(0,0);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (canvas == null) canvas = GetComponentInParent<Canvas>();
        content.SetParent(canvas.transform, true);
    }
}