using UnityEngine;
using UnityEngine.U2D.Animation;

public class FungibleTokenUIManager : MonoBehaviour {
    [SerializeField] BasicTokenUI tokenPrefab;
    [SerializeField] SpriteBuilderTokenUI spriteBuilderPrefab;
    [SerializeField] RequestSO request;
    [SerializeField] SpriteLibraryAsset library;
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
            item = Resources.Load<InventoryItem>($"Inventory/{pair.Key}");
            if (item == null) {
                Sprite img = Resources.Load<Sprite>($"Tokens/{pair.Key}");
                item = ScriptableObject.CreateInstance<InventoryItem>();
                item.Initialize(img, $"Token {pair.Key}", "Some token", pair.Value, pair.Key);
            } else item.SetAmount(pair.Value);
            tokenUI.Initialize(item);
        }
        
        request.ReadSmols();
        for(int i = 0; i < 6; i++) {
            var obj = Instantiate(spriteBuilderPrefab, new Vector3(0,0,0), Quaternion.identity, tokenContainer);
            var item = ScriptableObject.CreateInstance<EquippableAttributes>();
            item.SetAttributes(CollectionFetcher.Smols[i].attributes);
            item.SetEquipmentType(EquipmentType.Outfit);
            item.Initialize(null, "A token", "Some token", 1, i);
            obj.Initialize(item);
            obj.InitializeAttributes(library, CollectionFetcher.Smols[i].attributes);
        } 
    }
}