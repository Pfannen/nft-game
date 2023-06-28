using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;
using Web3Helpers;

public class SpriteController : MonoBehaviour {
    [SerializeField] FashionOutfit defaultOutfit;
    [SerializeField] GameObject spritesHolder;
    [SerializeField] SpriteRenderer spriteGO;
    FashionManager fashionManager;
    SpriteRenderer[] renderers;

    void Awake() {
        fashionManager = GetComponent<FashionManager>();
    }

    void OnEnable() {
        if (fashionManager) {
            fashionManager.ItemWorn += SetLayer;
            fashionManager.ItemRemoved += SetLayerNull;
        }
    }

    void OnDisable() {
        if (fashionManager) {
            fashionManager.ItemWorn -= SetLayer;
            fashionManager.ItemRemoved -= SetLayerNull;
        }
    }

    void Start() {
        if (fashionManager) {
            Sprite[] sprites = new Sprite[LayerHelper.NumLayers(fashionManager.WearableCollection)];
            for (int i = 0; i < sprites.Length; i++) sprites[i] = fashionManager.GetItem(i)?.Sprite;
            renderers = ImageBuilder.BuildSpriteLayers(sprites, spritesHolder);
        } else renderers = ImageBuilder.BuildSpriteLayersFromOutfit(defaultOutfit, spritesHolder);
    }

    private void SetOutfit(FashionOutfit outfit) {
        if (renderers == null) ImageBuilder.BuildSpriteLayersFromOutfit(outfit, spritesHolder);
        else ImageBuilder.SetLayersFromOutfit(outfit, spritesHolder, renderers);
    }

    private void SetLayer(FashionItem layerItem) {
        Debug.Log(layerItem);
        renderers[layerItem.LayerOrder].sprite = layerItem.Image;
    }

    private void SetLayerNull(int layer) {
        renderers[layer].sprite = null;
    }
}