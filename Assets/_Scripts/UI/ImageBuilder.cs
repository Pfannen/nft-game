using UnityEngine;
using UnityEngine.U2D.Animation;
using UnityEngine.UI;
using Web3Helpers;

public class ImageBuilder : MonoBehaviour {
    [SerializeField] GameObject prefab;
    [SerializeField] RequestSO request;
    [SerializeField] SpriteLibraryAsset library;

    void Start() {
        request.ReadSmols();
        for(int i = 0; i < 6; i++) SetImage(CollectionFetcher.Smols[i]);
    }

    public void SetImage(Smol smol) {
        if (smol == null) return;
        Attributes attributes = smol.attributes;
        GameObject obj = Instantiate(prefab, transform);
        obj.GetComponent<WearableOutfit>().SetAttributes(attributes); //Can make WearableOutfit set the sprites.
        Image[] childImages = obj.GetComponentsInChildren<Image>();
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