using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Tooltip : MonoBehaviour {
    public static CharacterLayerManager CharacterManager = null;
    public static EquipmentManager EquipmentManager = null;

    [SerializeField] TMP_Text description;
    [SerializeField] Button button;
    [SerializeField] TMP_Text buttonText;
    [SerializeField] Image background;
    [SerializeField] ItemDetailsDisplay itemDetails;

    [SerializeField] float descriptionPadding = 4f;
    [SerializeField] float descriptionButtonGap = 20f;

    RectTransform parentTransform;
    Vector2 buttonDimensions = new Vector2(0,0);
    InventoryItemUI current;

    void Start() {
        CharacterManager = SerializableCharacterManager.Instance;
        EquipmentManager = SerializableEquipment.Instance;
        parentTransform = GetComponent<RectTransform>();
        var buttonTransform = button.GetComponent<RectTransform>();
        buttonDimensions = new Vector2(buttonTransform.sizeDelta.x, buttonTransform.sizeDelta.y);
        DisableChildObjects();
    }

    void OnEnable() {
        InventoryItemUI.InventoryItemSelected += OnInventoryItemSelected;
    }

    void OnDisable() {
        InventoryItemUI.InventoryItemSelected -= OnInventoryItemSelected;
    }

    public void SetDescription(string text) {
        //description.gameObject.SetActive(true);
        description.text = text;
        //parentTransform.sizeDelta = new Vector2(350 + descriptionPadding * 2, description.preferredHeight + (descriptionPadding * 2) + 200);
        //description.rectTransform.sizeDelta = new Vector2(description.rectTransform.sizeDelta.x, description.preferredHeight);
        background.gameObject.SetActive(true);
        DisableButton();
    }

    public void SetButton(string text, UnityAction onButtonClick, bool buttonIsClicked) {
        //if (!button.gameObject.activeSelf) parentTransform.sizeDelta += new Vector2(0, buttonDimensions.y + descriptionButtonGap);
        button.gameObject.SetActive(true);
        if (buttonIsClicked) button.image.color = button.colors.disabledColor; 
        else button.image.color = button.colors.normalColor;
        buttonText.text = text;
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(onButtonClick);
    }

    private void OnInventoryItemSelected(InventoryItemUI item) {
        if (current == item) {
            DisableChildObjects();
            current = null;
        } else {
            current = item;
            var actualItem = item.InventoryItem;
            EnableChildObjects();
            SetDescription(actualItem.Description);
            itemDetails.SetItem((BasicTokenUI)item);
            if (actualItem.IsUsable) {
                SetButton("Use", actualItem.ItemMethod, false);
            }
            //var itemTransform = item.GetComponent<RectTransform>();
            //parentTransform.position = new Vector2(itemTransform.position.x, itemTransform.position.y - itemTransform.sizeDelta.y);
            parentTransform.anchoredPosition = Vector2.zero;
        }
    }

    private void DisableChildObjects() {
        for (int i = 0; i < transform.childCount; i++) transform.GetChild(i).gameObject.SetActive(false);
    }

    private void EnableChildObjects() {
        for (int i = 0; i < transform.childCount; i++) transform.GetChild(i).gameObject.SetActive(true);
    }

    private void DisableTextAndButton() {
        DisableButton();
        DisableText();
        background.gameObject.SetActive(false);
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