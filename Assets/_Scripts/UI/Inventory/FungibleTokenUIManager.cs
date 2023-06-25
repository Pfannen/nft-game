using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.U2D.Animation;
using Web3Helpers;
using System;

public class FungibleTokenUIManager : MonoBehaviour {
    [SerializeField] BasicTokenUI tokenPrefab;
    [SerializeField] SpriteBuilderTokenUI spriteBuilderPrefab;
    [SerializeField] RequestSO request;
    [SerializeField] SpriteLibraryAsset femaleSmachoLibrary;
    [SerializeField] SpriteLibraryAsset maleSmachoLibrary;
    PlayerInventory inventory;
    RectTransform tokenContainer;

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
        EquippableAttributes[] smachos = Resources.LoadAll<EquippableAttributes>("Inventory/Smacho/");
        Dictionary<string, EquippableAttributes> storedSmachos = new Dictionary<string, EquippableAttributes>();
        foreach (var smacho in smachos) storedSmachos.Add(smacho.name, smacho);  
        
        foreach(Smol smacho in CollectionFetcher.Smols) {
            var obj = Instantiate(spriteBuilderPrefab, new Vector3(0,0,0), Quaternion.identity, tokenContainer);
            if (storedSmachos.TryGetValue(smacho.tokenId, out EquippableAttributes smachoItem)) {
                obj.Initialize(smachoItem, 1);
                obj.InitializeAttributes(smachoItem.Library, smachoItem.Attributes);
                Debug.Log("Smacho found");
            } else {
                var lib = smacho.attributes.Gender == "female" ? femaleSmachoLibrary : maleSmachoLibrary;
                var item = ScriptableObject.CreateInstance<EquippableAttributes>();
                item.SetAttributes(smacho.attributes, lib);
                item.Initialize(null, "A token", $"Smol token {smacho.tokenId}", "Smacho", Int32.Parse(smacho.tokenId));
                AssetDatabase.CreateAsset(item, $"Assets/Resources/Inventory/Smacho/{smacho.tokenId}.asset");
                obj.Initialize(item, 1);
                obj.InitializeAttributes(lib, smacho.attributes);
            }
        } 
    }
}