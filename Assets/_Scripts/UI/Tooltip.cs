using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Tooltip : MonoBehaviour {
    [SerializeField] TMP_Text description;
    [SerializeField] Button button;
    [SerializeField] TMP_Text buttonText;
    [SerializeField] float descriptionPadding = 4f;
    [SerializeField] float descriptionButtonGap = 20f;
    [SerializeField] EquipmentManager equipmentManager;
    [SerializeField] CharacterLayerManager fashionManager;

    public static EquipmentManager EquipmentManager;
    public static CharacterLayerManager FashionManager;

    RectTransform parentTransform;
    Vector2 buttonDimensions = new Vector2(0,0);
    InventoryItemUI current;

    void Start() {
        EquipmentManager = equipmentManager;
        FashionManager = fashionManager;
        parentTransform = GetComponent<RectTransform>();
        var buttonTransform = button.GetComponent<RectTransform>();
        buttonDimensions = new Vector2(buttonTransform.sizeDelta.x, buttonTransform.sizeDelta.y);
        DisableTextAndButton();
    }

    void OnEnable() {
        InventoryItemUI.InventoryItemSelected += OnInventoryItemSelected;
    }

    void OnDisable() {
        InventoryItemUI.InventoryItemSelected -= OnInventoryItemSelected;
    }

    public void SetDescription(string text) {
        description.gameObject.SetActive(true);
        description.text = text;
        parentTransform.sizeDelta = new Vector2(350 + descriptionPadding * 2, description.preferredHeight + (descriptionPadding * 2));
        description.rectTransform.sizeDelta = new Vector2(350, description.preferredHeight);
        DisableButton();
    }

    public void SetButton(string text, UnityAction onButtonClick, bool buttonIsClicked) {
        if (!button.gameObject.activeSelf) parentTransform.sizeDelta += new Vector2(0, buttonDimensions.y + descriptionButtonGap);
        button.gameObject.SetActive(true);
        if (buttonIsClicked) button.image.color = button.colors.disabledColor; 
        else button.image.color = button.colors.normalColor;
        buttonText.text = text;
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(onButtonClick);
    }

    private void OnInventoryItemSelected(InventoryItemUI item) {
        if (current == item) {
            DisableTextAndButton();
            current = null;
        } else {
            current = item;
            var actualItem = item.InventoryItem;
            SetDescription(actualItem.Description);
            if (actualItem.IsUsable) {
                SetButton("Use", actualItem.ItemMethod, false);
            }
            var itemTransform = item.GetComponent<RectTransform>();
            parentTransform.position = new Vector2(itemTransform.position.x, itemTransform.position.y - itemTransform.sizeDelta.y);
        }
    }

    private void DisableTextAndButton() {
        DisableButton();
        DisableText();
    }

    private void DisableButton() {
        if (button.gameObject.activeSelf) {
            button.onClick.RemoveAllListeners();
            button.gameObject.SetActive(false);
        }
    }

    private void DisableText() {
        description.gameObject.SetActive(false);
    }
}