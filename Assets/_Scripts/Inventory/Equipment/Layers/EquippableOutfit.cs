using System.Collections.Generic;
using UnityEngine;
using Web3Helpers;

[CreateAssetMenu(fileName = "Outfit", menuName = "Macho/Wearables/Outfit", order = 1)]
public class EquippableOutfit : EquippableItem {
    bool outfitSet = false;
    [SerializeField] EquippableLayerItem[] outfitLayers;

    public void SetCollection(CollectionIdentifier collection) {
        this.collection = collection;
    }

    public void SetOutfitLayers(EquippableLayerItem[] outfitLayers) {
        if (!outfitSet) this.outfitLayers = outfitLayers;
        outfitSet = true;
    }

    public IEnumerable<EquippableLayerItem> GetOutfitLayers() {
        return outfitLayers;
    }
}