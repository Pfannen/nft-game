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
        DestroyChildObjects();
        if (fashionManager) {
            CreateRenderers(fashionManager.WearableCollection);
            for (int i = 0; i < LayerHelper.NumLayers(fashionManager.WearableCollection); i++) {
                var item = fashionManager.GetItem(i);
                if (!item) SetLayerNull(i);
                else SetLayer(item);
            }
        } else {
            CreateRenderers(defaultOutfit.Collection);
            SetOutfit(defaultOutfit);
        }
    }

    private void SetOutfit(FashionOutfit outfit) {
        for (int i = 0; i < renderers.Length; i++) SetLayerNull(i);
        foreach (var layerItem in outfit.GetOutfitLayers()) SetLayer(layerItem);
    }

    private void DestroyChildObjects() {
        foreach(Transform child in spritesHolder.transform) Destroy(child.gameObject);
    }

    private void CreateRenderers(CollectionIdentifier collection) {
        renderers = new SpriteRenderer[LayerHelper.NumLayers(collection)];
        for (int i = 0; i < renderers.Length; i++) {
            renderers[i] = Instantiate(spriteGO, spritesHolder.transform.position, Quaternion.identity, spritesHolder.transform);
            renderers[i].gameObject.name = i.ToString();
            renderers[i].sortingLayerName = i.ToString();
        }
    }

    private void SetLayer(FashionItem layerItem) {
        Debug.Log(layerItem);
        renderers[layerItem.LayerOrder].sprite = layerItem.Image;
    }

    private void SetLayerNull(int layer) {
        renderers[layer].sprite = null;
    }
}