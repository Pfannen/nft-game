using System;
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

    public static void BuildImageLayers(Sprite[] sprites, RectTransform parent, bool maintainDimensions) {
        DeleteChildren(parent.gameObject);
        Vector2? dimensions = null;
        for (int i = 0; i < sprites.Length; i++) {
            Sprite sprite = sprites[i];
            GameObject gO = new GameObject(i.ToString(), typeof(RectTransform));
            var renderer = gO.AddComponent<Image>();
            renderer.sprite = sprite;
            if (sprite == null) renderer.color = new Color(0,0,0,0);
            else if (maintainDimensions) {
                if (dimensions == null) {
                    float factor = Math.Min(parent.sizeDelta.x / sprite.rect.size.x, parent.sizeDelta.y / sprite.rect.size.y);
                    dimensions = new Vector2(sprite.rect.size.x * factor, sprite.rect.size.y * factor);
                }
                RectTransform spriteTransform = gO.GetComponent<RectTransform>();
                spriteTransform.sizeDelta = dimensions.Value;
                spriteTransform.position = Vector2.zero;
            }
            gO.transform.SetParent(parent.transform, false);
        }
    }

    public static void BuildImageLayersFromOutfit(FashionOutfit outfit, RectTransform parent, bool maintainDimensions) {
        Sprite[] sprites = new Sprite[LayerHelper.NumLayers(outfit.Collection)];
        foreach (var item in outfit.GetOutfitLayers()) if (item != null) sprites[item.LayerOrder] = item.Sprite;
        BuildImageLayers(sprites, parent, maintainDimensions);
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

    public static FashionOutfit BuildOutfitFromSmol(Smol smol, FashionLibrary lib) {
        Attributes attr = smol.attributes;
        FashionItem[] items = new FashionItem[LayerHelper.NumLayers(CollectionIdentifier.Smol)];
        items[0] = lib.GetLayerItem("Body", attr.Body);
        items[1] = lib.GetLayerItem("Clothes", attr.Clothes);
        items[2] = lib.GetLayerItem("Glasses", attr.Glasses);
        items[3] = lib.GetLayerItem("Hat", attr.Hat);
        items[4] = lib.GetLayerItem("Mouth", attr.Mouth);
        FashionOutfit outfit = ScriptableObject.CreateInstance<FashionOutfit>();
        outfit.SetOutfitLayers(items, lib);
        outfit.Initialize(null, $"Smol {smol.tokenId}", items[0].ItemName, CollectionIdentifier.Smol, Int32.Parse(smol.tokenId));
        return outfit;
    }
}