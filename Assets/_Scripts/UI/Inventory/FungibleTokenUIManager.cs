using UnityEngine;

public class FungibleTokenUIManager : MonoBehaviour {
    [SerializeField] TokenUI tokenPrefab;
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
    }
}