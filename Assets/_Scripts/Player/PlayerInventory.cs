using System.Collections.Generic;
using HttpRequests.CollectibleFormats;
using UnityEngine;

public class PlayerInventory : MonoBehaviour {
    int userId = 1;
    Dictionary<int, int> playerCollectibles = new Dictionary<int, int>();

    /* void Awake() {
        //Create 'Collectible' class and store player collectibles in 'playerCollectibles'
    } */

    void Start() {
        var inventories = FindObjectsOfType<PlayerInventory>();
        if (inventories.Length == 1) DontDestroyOnLoad(gameObject);
        else Destroy(gameObject);
    }

    void Update() {
        if (Input.GetKeyDown("c")) ClaimCollectibles();
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

    private async void ClaimCollectibles() {
        if (playerCollectibles.Count != 0) {
            List<UserToken> tokens = new List<UserToken>();
            foreach (var token in playerCollectibles) {
                tokens.Add(new UserToken(token.Key, token.Value));
                Debug.Log(token.Value);
            }
            try {
                await HttpRequest.Post("http://localhost:3000/database/claim", new TokenBody(userId, tokens.ToArray()));
                playerCollectibles = new Dictionary<int, int>();
                Debug.Log("Collectibles claimed");
            } catch (System.Exception ex) {
                Debug.Log(ex.Message);
            }
        } else Debug.Log("No collectibles to claim");
    }

}