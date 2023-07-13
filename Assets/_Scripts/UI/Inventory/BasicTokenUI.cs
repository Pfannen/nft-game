using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class BasicTokenUI : InventoryItemUI, IRaycastable, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler {
    [SerializeField] Image image;
    [SerializeField] TMP_Text text;
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

    public override void Initialize(InventoryItem inventoryItem, int amount)
    {
        base.Initialize(inventoryItem, amount);
        image.sprite = inventoryItem.Image;
        text.text = " x" + amount;
    }

    public void OnRaycast() {
        OnInventoryItemSelected(this);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 0.6f;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("OnDrag");
        content.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("OnEndDrag");
        canvasGroup.alpha = 1f;
        content.SetParent(parent.transform, false);
        canvasGroup.blocksRaycasts = true;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("OnPointerDown");
        canvasGroup.blocksRaycasts = false;
        if (canvas == null) canvas = GetComponentInParent<Canvas>();
        content.SetParent(canvas.transform, true);
    }
}