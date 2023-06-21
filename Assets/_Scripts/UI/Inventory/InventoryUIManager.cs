using UnityEngine;

public class InventoryUIManager : MonoBehaviour {
    [SerializeField] RectTransform coinContainer;
    [SerializeField] CoinUI coinPrefab;
    PlayerInventory inventory;

    void Awake() {
        inventory = FindObjectOfType<PlayerInventory>();
        inventory.CollectiblesUpdated += OnCollectiblesUpdated;
    }

    void OnDestroy() {
        inventory.CollectiblesUpdated -= OnCollectiblesUpdated;
    }

    private void OnCollectiblesUpdated() {
        foreach(var pair in inventory.PlayerCollectibles) {
            var coinUI = Instantiate(coinPrefab, new Vector3(0,0,0), Quaternion.identity, coinContainer);
            coinUI.SetText('x' + pair.Value.ToString());
        }
    }
}