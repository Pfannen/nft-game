using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CoinUI : MonoBehaviour {
    [SerializeField] Image image;
    [SerializeField] TMP_Text text;

    public void SetImage(Image image) {
        this.image = image;
    }

    public void SetText(string text) {
        this.text.text = text;
    }
}