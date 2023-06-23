using UnityEngine;
using UnityEngine.U2D.Animation;
using UnityEngine.UI;
using Web3Helpers;

public static class ImageBuilder {
    public static void SetImages(GameObject parent, Attributes attributes, SpriteLibraryAsset library) {
        if (attributes == null) return;
        Image[] childImages = parent.GetComponentsInChildren<Image>();
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