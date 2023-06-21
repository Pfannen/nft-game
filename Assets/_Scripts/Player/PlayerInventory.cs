using System;
using System.Collections.Generic;
using HttpRequests.CollectibleFormats;
using UnityEngine;

public class PlayerInventory : MonoBehaviour {
    string session = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJhY2NvdW50SWQiOjEsImlhdCI6MTY4NzIwNzMxNH0.QF87oxFi2bh0eoU0SHIQdVg_JRNv6T2Bzd3yv7_Lui8";
    Dictionary<int, int> claimedCollectibles = new Dictionary<int, int>();
    Dictionary<int, int> unclaimedCollectibles = new Dictionary<int, int>();

    public Dictionary<int,int> PlayerCollectibles => claimedCollectibles;
    public event Action CollectiblesUpdated;

    void Awake() {
        //Create 'Collectible' class and store player collectibles in 'playerCollectibles'
        GetCollectibleBalances();
    }

    /* void Start() {
        var inventories = FindObjectsOfType<PlayerInventory>();
        if (inventories.Length == 1) DontDestroyOnLoad(gameObject);
        else Destroy(gameObject);
    } */

    void Update() {
        if (Input.GetKeyDown("c")) ClaimCollectibles();
    }

    void OnEnable() {
        PersistentCollectible.OnCollect += UpdateUnclaimedCollectibles;
    }

    void OnDisable() {
        PersistentCollectible.OnCollect -= UpdateUnclaimedCollectibles;
    }

    private async void ClaimCollectibles() {
        if (unclaimedCollectibles.Count != 0) {
            List<UserToken> tokens = new List<UserToken>();
            foreach (var token in unclaimedCollectibles) {
                tokens.Add(new UserToken(token.Key, token.Value));
            }
            try {
                await HttpRequest.Post("http://localhost:3000/database/claim", new TokenBody(session, tokens.ToArray()));
                UpdateClaimedCollectibles(tokens.ToArray(), true);
                unclaimedCollectibles = new Dictionary<int, int>();
                Debug.Log("Collectibles claimed");
            } catch (System.Exception ex) {
                Debug.Log(ex.Message);
            }
        } else Debug.Log("No collectibles to claim");
    }

    private async void GetCollectibleBalances() {
        UserToken[] playerTokens = await HttpRequest.Get<UserToken[]>($"http://localhost:3000/database/user-tokens?session={session}");
        UpdateClaimedCollectibles(playerTokens, false);
    }

    private void UpdateUnclaimedCollectibles(int collectibleId, int amount) {
        if (unclaimedCollectibles.ContainsKey(collectibleId)) unclaimedCollectibles[collectibleId] += amount;
        else unclaimedCollectibles.Add(collectibleId, amount);
        CollectiblesUpdated?.Invoke();
    }

    private void UpdateClaimedCollectibles(UserToken[] playerTokens, bool addToCurrent) {
        if (addToCurrent) {
            foreach(UserToken token in playerTokens) {
                if (claimedCollectibles.ContainsKey(token.tokenId)) claimedCollectibles[token.tokenId] += token.amount;
                else claimedCollectibles.Add(token.tokenId, token.amount);
            }
        } else {
            foreach(UserToken token in playerTokens) {
                if (claimedCollectibles.ContainsKey(token.tokenId)) claimedCollectibles[token.tokenId] = token.amount;
                else claimedCollectibles.Add(token.tokenId, token.amount);
            }
        }
        CollectiblesUpdated?.Invoke();
        //LogCollectibles();
    }

    private void LogCollectibles() {
        foreach(var pair in claimedCollectibles) Debug.Log($"TokenId: {pair.Key}; Amount: {pair.Value}");
    }
}