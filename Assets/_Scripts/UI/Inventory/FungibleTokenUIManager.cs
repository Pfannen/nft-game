using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.U2D.Animation;
using Web3Helpers;
using System;
using UnityEngine.SceneManagement;

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

    void Update() {
        if (Input.GetKeyDown(KeyCode.P)) SceneManager.LoadScene(1);
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
            FashionOutfit outfit = ImageBuilder.BuildOutfitFromSmol(smol, lib);
            var obj = Instantiate(tokenPrefab, new Vector3(0,0,0), Quaternion.identity, tokenContainer);
            ImageBuilder.BuildImageLayersFromOutfit(outfit, obj.GetComponent<RectTransform>(), true);
            obj.Initialize(outfit, 1);
            //obj.InitializeAttributes(attr.Gender == "male" ? maleSmachoLibrary : femaleSmachoLibrary, attr);
        }
    }
}