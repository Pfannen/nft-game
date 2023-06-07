using UnityEngine;
using UnityEngine.U2D.Animation;
using UnityEngine.UI;

public class ImageBuilder : MonoBehaviour {
    [SerializeField] GameObject prefab;
    [SerializeField] RequestSO request;
    SpriteLibrary library;

    void Start() {
        library = GetComponent<SpriteLibrary>();
        request.ReadSmols();
        for(int i = 0; i < 6; i++) SetImage(DataFetcher.Smols[i]);
    }

    public void SetImage(Smol smol) {
        if (smol == null) return;
        GameObject obj = Instantiate(prefab, transform);
        Image[] childImages = obj.GetComponentsInChildren<Image>();
        Attributes attributes = smol.attributes;
        childImages[1].sprite = library.GetSprite("Body", attributes.Body);
        childImages[2].sprite = library.GetSprite("Eye", attributes.Glasses);
        childImages[3].sprite = library.GetSprite("Hat", attributes.Hat);
        childImages[4].sprite = library.GetSprite("Shirt", attributes.Clothes);
        childImages[5].sprite = library.GetSprite("Mouth", attributes.Mouth);
        for(int i = 1; i < 6; i++) {
            if (childImages[i].sprite == null) childImages[i].color = new Color(0,0,0,0);
        }
    }
}