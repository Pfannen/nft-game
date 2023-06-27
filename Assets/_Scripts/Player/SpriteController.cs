using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;
using Web3Helpers;

public class SpriteController : MonoBehaviour {
    private static Attributes defaultAttributes = new Attributes();

    [SerializeField] RequestSO request;
    [SerializeField] SpriteLibrary library;
    SpriteResolver[] resolvers;
    int curFit = 0;

    [SerializeField] EquippableOutfit defaultOutfit;
    [SerializeField] GameObject spritesHolder;
    [SerializeField] SpriteRenderer spriteGO;
    EquipmentManager equipmentManager;
    CollectionIdentifier equippedOutfitCollection = CollectionIdentifier.Smol;
    SpriteRenderer[] renderers;

    void Awake() {
        equipmentManager = GetComponent<EquipmentManager>();
        //resolvers = GetComponentsInChildren<SpriteResolver>();
        DestroyChildObjects();
        CreateRenderers(defaultOutfit.Collection);
        SetOutfit(defaultOutfit);
    }

    private void SetOutfit(EquippableOutfit outfit) {
        if (equippedOutfitCollection != outfit.Collection) {
            DestroyChildObjects();
            CreateRenderers(outfit.Collection);
            equippedOutfitCollection = outfit.Collection;
        }
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

    private bool SetLayer(EquippableLayerItem layerItem) {
        if (equippedOutfitCollection != layerItem.Collection) return false;
        renderers[layerItem.LayerOrder].sprite = layerItem.Image;
        return true;
    }

    private void SetLayerNull(int layer) {
        renderers[layer].sprite = null;
    }


    /* void Start() {
        var outfit = equipmentManager.GetEquipment(EquipmentType.Outfit) as EquippableAttributes;
        if (outfit == null) SetOutfit(defaultAttributes, null);
        else SetOutfit(outfit.Attributes, outfit.Library);
    } */

    /* public void SetOutfit(Attributes attributes, SpriteLibraryAsset libraryAsset) {
        if (attributes == null) return;
        if (libraryAsset != null) library.spriteLibraryAsset = libraryAsset;
        resolvers[0].SetCategoryAndLabel("Hat", attributes.Hat);
        resolvers[1].SetCategoryAndLabel("Glasses", attributes.Glasses);
        resolvers[2].SetCategoryAndLabel("Body", attributes.Body);
        resolvers[3].SetCategoryAndLabel("Mouth", attributes.Mouth);
        resolvers[4].SetCategoryAndLabel("Clothes", attributes.Clothes);
    } */
}