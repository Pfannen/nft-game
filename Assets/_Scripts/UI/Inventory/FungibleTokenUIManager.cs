using UnityEngine;
using UnityEngine.U2D.Animation;
using UnityEngine.SceneManagement;

public class FungibleTokenUIManager : MonoBehaviour {
    [SerializeField] DraggableTokenUI tokenPrefab;
    [SerializeField] RequestSO request;
    [SerializeField] SpriteLibraryAsset femaleSmachoLibrary;
    [SerializeField] SpriteLibraryAsset maleSmachoLibrary;
    PlayerInventory inventory;
    RectTransform tokenContainer;

    [SerializeField] CharacterLayerLibrary mFL;
    [SerializeField] CharacterLayerLibrary fFL;

    void Awake() {
        tokenContainer = GetComponent<RectTransform>();
        inventory = FindObjectOfType<PlayerInventory>();
        if (inventory.InventoryLoaded && tokenContainer.childCount == 0) OnCollectiblesUpdated();
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
            CharacterPreset outfit = ImageBuilder.BuildCharacterFromSmol(smol, lib);
            var obj = Instantiate(tokenPrefab, new Vector3(0,0,0), Quaternion.identity, tokenContainer);
            ImageBuilder.BuildImageLayersFromOutfit(outfit, obj.Content, true);
            obj.Initialize(outfit, 1);
            //obj.InitializeAttributes(attr.Gender == "male" ? maleSmachoLibrary : femaleSmachoLibrary, attr);
        }
    }
}