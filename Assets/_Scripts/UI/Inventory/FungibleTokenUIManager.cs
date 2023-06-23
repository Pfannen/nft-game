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
            Sprite img = Resources.Load<Sprite>($"Tokens/{pair.Key}");
            var tokenUI = Instantiate(tokenPrefab, new Vector3(0,0,0), Quaternion.identity, tokenContainer);
            tokenUI.Initialize(pair.Key, img, " x" + pair.Value.ToString());
        }
        request.ReadSmols();
        for(int i = 0; i < 6; i++) {
            var obj = Instantiate(spriteBuilderPrefab, new Vector3(0,0,0), Quaternion.identity, tokenContainer);
            obj.InitializeAttributes(library, CollectionFetcher.Smols[i].attributes);
        } 
    }
}