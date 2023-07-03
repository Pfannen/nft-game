using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Web3Helpers;

public class CharacterInventory : MonoBehaviour {
    public static CharacterInventory Instance { get; private set; }

    Dictionary<CollectionIdentifier, object[]> characters = null;

    void Awake() {
        Instance = this;
    }

    public async Task<List<KeyValuePair<CollectionIdentifier, object>>> GetCharacters() {
        if (characters == null) await FetchCharacters();
        var list = new List<KeyValuePair<CollectionIdentifier, object>>();
        foreach (var pair in characters) {
            foreach (var character in pair.Value) {
                list.Add(new KeyValuePair<CollectionIdentifier, object>(pair.Key, character));
            }
        }
        return list;
    }

    private async Task FetchCharacters() {
        Smol[] smols = await CollectionFetcher.GetSmols(CollectionFetcher.TestingAddress);
        characters = new Dictionary<CollectionIdentifier, object[]>();
        characters.Add(CollectionIdentifier.Smol, smols);
    }
}