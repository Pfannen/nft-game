using System.Collections.Generic;
using UnityEngine;
using Web3Helpers;

[CreateAssetMenu(fileName = "Fashion Outfit", menuName = "Macho/Wearables/Outfit", order = 1)]
public class FashionOutfit : InventoryItem {
    bool outfitSet = false;
    [SerializeField] FashionItem[] outfitLayers;
    [SerializeField] FashionLibrary fashionLibrary;

    public FashionLibrary FashionLibrary => fashionLibrary;
    public override bool IsUsable => true;

    public void SetCollection(CollectionIdentifier collection) {
        this.collection = collection;
    }

    public void SetOutfitLayers(FashionItem[] outfitLayers, FashionLibrary fashionLibrary) {
        if (!outfitSet) this.outfitLayers = outfitLayers;
        outfitSet = true;
        this.fashionLibrary = fashionLibrary;
    }

    public IEnumerable<FashionItem> GetOutfitLayers() {
        return outfitLayers;
    }

    public void EquipOutfit(FashionManager manager) {
        manager.WearOutfit(this);
    }

    protected override void UseItemMethod() {
        EquipOutfit(Tooltip.FashionManager);
    }
}