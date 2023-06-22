using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Web3Helpers;
using UnityEngine.SceneManagement;

public class Tooltip : MonoBehaviour {
    [SerializeField] TMP_Text description;
    [SerializeField] Button button;
    [SerializeField] TMP_Text buttonText;
    [SerializeField] float descriptionPadding = 4f;
    [SerializeField] float descriptionButtonGap = 20f;

    RectTransform parentTransform;
    Vector2 buttonDimensions = new Vector2(0,0);

    void Start() {
        parentTransform = GetComponent<RectTransform>();
        var buttonTransform = button.GetComponent<RectTransform>();
        buttonDimensions = new Vector2(buttonTransform.sizeDelta.x, buttonTransform.sizeDelta.y);
        SetDescription("fjdsklfjas;lkfj ldfj asfj aksf ;asfsad fdsa ");
        EnableButton("Equip", () => { Debug.Log("Attempting to equip item"); }, false);
    }

    void OnEnable() {
        WearableOutfit.OutfitClicked += OnOutfitClick;
    }

    void OnDisable() {
        WearableOutfit.OutfitClicked -= OnOutfitClick;
    }

    public void SetDescription(string text) {
        description.text = text;
        parentTransform.sizeDelta = new Vector2(350 + descriptionPadding * 2, description.preferredHeight + (descriptionPadding * 2));
        description.rectTransform.sizeDelta = new Vector2(350, description.preferredHeight);
        DisableButton();
    }

    public void EnableButton(string text, UnityAction onButtonClick, bool buttonIsClicked) {
        button.gameObject.SetActive(true);
        parentTransform.sizeDelta += new Vector2(0, buttonDimensions.y + descriptionButtonGap);
        if (buttonIsClicked) button.image.color = button.colors.disabledColor; 
        else button.image.color = button.colors.normalColor;
        buttonText.text = text;
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(onButtonClick);
    }

    private void OnOutfitClick(WearableOutfit outfit) {
        SetDescription(outfit.Attributes.Background);
        if (SpriteController.SelectedOutfit == outfit.Attributes) {
            EnableButton("Play", () => { SceneManager.LoadScene(1); }, false);
        } else {
            EnableButton("Equip", () => { SpriteController.SelectedOutfit = outfit.Attributes; EnableButton("Play", () => { SceneManager.LoadScene(1); }, false); }, false);
        }
        parentTransform.position = outfit.gameObject.GetComponent<RectTransform>().position;
    }

    private void DisableButton() {
        if (button.gameObject.activeSelf) {
            button.onClick.RemoveAllListeners();
            button.gameObject.SetActive(false);
        }
    }
}