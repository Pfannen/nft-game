using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;
using UnityEngine.UI;
using Web3Helpers;

public static class ImageBuilder {
    public static void SetImages(GameObject parent, Attributes attributes, SpriteLibraryAsset library) {
        if (attributes == null) return;
        Image[] childImages = parent.GetComponentsInChildren<Image>();
        childImages[1].sprite = library.GetSprite("Body", attributes.Body);
        childImages[2].sprite = library.GetSprite("Glasses", attributes.Glasses);
        childImages[3].sprite = library.GetSprite("Hat", attributes.Hat);
        childImages[4].sprite = library.GetSprite("Clothes", attributes.Clothes);
        childImages[5].sprite = library.GetSprite("Mouth", attributes.Mouth);
        for(int i = 1; i < 6; i++) {
            if (childImages[i].sprite == null) childImages[i].color = new Color(0,0,0,0);
        }
    }

    public static SpriteRenderer[] BuildSpriteLayers(Sprite[] sprites, GameObject parent) {
        DeleteChildren(parent);
        SpriteRenderer[] renderers = new SpriteRenderer[sprites.Length];
        for (int i = 0; i < sprites.Length; i++) {
            GameObject gO = new GameObject(i.ToString());
            var renderer = gO.AddComponent<SpriteRenderer>();
            renderer.sprite = sprites[i];
            renderer.sortingLayerName = i.ToString();
            gO.transform.SetParent(parent.transform);
            gO.transform.position = parent.transform.position;
            renderers[i] = renderer;
        }
        return renderers;
    }

    public static void BuildImageLayers(Sprite[] sprites, GameObject parent) {
        DeleteChildren(parent);
        for (int i = 0; i < sprites.Length; i++) {
            GameObject gO = new GameObject(i.ToString(), typeof(RectTransform));
            var renderer = gO.AddComponent<Image>();
            renderer.sprite = sprites[i];
            if (sprites[i] == null) renderer.color = new Color(0,0,0,0);
            gO.transform.SetParent(parent.transform);
        }
    }

    public static void BuildImageLayersFromOutfit(FashionOutfit outfit, GameObject parent) {
        Sprite[] sprites = new Sprite[LayerHelper.NumLayers(outfit.Collection)];
        foreach (var item in outfit.GetOutfitLayers()) if (item != null) sprites[item.LayerOrder] = item.Sprite;
        BuildImageLayers(sprites, parent);
    }

    public static SpriteRenderer[] BuildSpriteLayersFromOutfit(FashionOutfit outfit, GameObject parent) {
        Sprite[] sprites = new Sprite[LayerHelper.NumLayers(outfit.Collection)];
        foreach (var item in outfit.GetOutfitLayers()) if (item != null) sprites[item.LayerOrder] = item.Sprite;
        return BuildSpriteLayers(sprites, parent);
    }


    public static bool SetLayersFromOutfit(FashionOutfit outfit, GameObject parent, SpriteRenderer[] renderers) {
        if (renderers.Length != LayerHelper.NumLayers(outfit.Collection)) return false;
        foreach (var renderer in renderers) renderer.sprite = null;
        foreach (var item in outfit.GetOutfitLayers()) renderers[item.LayerOrder].sprite = item.Sprite;
        return true;
    }

    private static void DeleteChildren(GameObject parent) {
        foreach (Transform obj in parent.transform) MonoBehaviour.Destroy(obj.gameObject);
    }
}