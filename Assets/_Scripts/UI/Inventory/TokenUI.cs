using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TokenUI : MonoBehaviour {
    [SerializeField] Image image;
    [SerializeField] TMP_Text text;

    public void SetImage(Sprite image) {
        this.image.sprite = image;
    }

    public void SetText(string text) {
        this.text.text = text;
    }
}