using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.U2D.Animation;
using Web3Helpers;
using System;

public class FungibleTokenUIManager : MonoBehaviour {
    [SerializeField] BasicTokenUI tokenPrefab;
    //[SerializeField] SpriteBuilderTokenUI spriteBuilderPrefab;
    [SerializeField] RequestSO request;
    [SerializeField] SpriteLibraryAsset femaleSmachoLibrary;
    [SerializeField] SpriteLibraryAsset maleSmachoLibrary;
    PlayerInventory inventory;
    RectTransform tokenContainer;

    [SerializeField] FashionLibrary mFL;
    [SerializeField] FashionLibrary fFL;

    void Awake() {
        tokenContainer = GetComponent<RectTransform>();
        inventory = FindObjectOfType<PlayerInventory>();
        inventory.CollectiblesUpdated += OnCollectiblesUpdated;
    }

    void OnDestroy() {
        inventory.CollectiblesUpdated -= OnCollectiblesUpdated;
    }

    private void OnCollectiblesUpdated() {
        foreach(var pair in inventory.PlayerCollectibles) {
            var tokenUI = Instantiate(tokenPrefab, new Vector3(0,0,0), Quaternion.identity, tokenContainer);
            InventoryItem item;
            item = Resources.Load<InventoryItem>($"Inventory/Macho/{pair.Key}");
            tokenUI.Initialize(item, pair.Value);
        }
        
        request.ReadSmols();

        foreach (var smol in CollectionFetcher.Smols) {
            var attr = smol.attributes;
            var lib = attr.Gender == "male" ? mFL : fFL;
            FashionItem[] items = new FashionItem[LayerHelper.NumLayers(CollectionIdentifier.Smol)];
            items[0] = lib.GetLayerItem("Body", attr.Body);
            items[1] = lib.GetLayerItem("Clothes", attr.Clothes);
            items[2] = lib.GetLayerItem("Glasses", attr.Glasses);
            items[3] = lib.GetLayerItem("Hat", attr.Hat);
            items[4] = lib.GetLayerItem("Mouth", attr.Mouth);
            FashionOutfit outfit = ScriptableObject.CreateInstance<FashionOutfit>();
            outfit.SetOutfitLayers(items, lib);
            outfit.Initialize(null, $"Smol {smol.tokenId}", items[0].ItemName, CollectionIdentifier.Smol, Int32.Parse(smol.tokenId));
            var obj = Instantiate(tokenPrefab, new Vector3(0,0,0), Quaternion.identity, tokenContainer);
            ImageBuilder.BuildImageLayersFromOutfit(outfit, obj.gameObject);
            obj.Initialize(outfit, 1);
            //obj.InitializeAttributes(attr.Gender == "male" ? maleSmachoLibrary : femaleSmachoLibrary, attr);
        }
    }
}