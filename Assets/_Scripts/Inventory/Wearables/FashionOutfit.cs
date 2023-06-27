using System.Collections.Generic;
using UnityEngine;
using Web3Helpers;

[CreateAssetMenu(fileName = "Fashion Outfit", menuName = "Macho/Wearables/Outfit", order = 1)]
public class FashionOutfit : InventoryItem {
    bool outfitSet = false;
    [SerializeField] FashionItem[] outfitLayers;

    public void SetCollection(CollectionIdentifier collection) {
        this.collection = collection;
    }

    public void SetOutfitLayers(FashionItem[] outfitLayers) {
        if (!outfitSet) this.outfitLayers = outfitLayers;
        outfitSet = true;
    }

    public IEnumerable<FashionItem> GetOutfitLayers() {
        return outfitLayers;
    }
}