using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour {
    Dictionary<int, int> playerCollectibles = new Dictionary<int, int>();

    /* void Awake() {
        //Create 'Collectible' class and store player collectibles in 'playerCollectibles'
    } */

    void Start() {
        var inventories = FindObjectsOfType<PlayerInventory>();
        if (inventories.Length == 1) DontDestroyOnLoad(gameObject);
        else Destroy(gameObject);
    }

    void OnEnable() {
        PersistentCollectible.OnCollect += UpdateCollectible;
    }

    void OnDisable() {
        PersistentCollectible.OnCollect -= UpdateCollectible;
    }

    private void UpdateCollectible(int collectibleId, int amount) {
        if (playerCollectibles.ContainsKey(collectibleId)) playerCollectibles[collectibleId] += amount;
        else playerCollectibles.Add(collectibleId, amount);
        Debug.Log(playerCollectibles[collectibleId]);
    }

}