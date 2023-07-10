using System.Collections.Generic;
using UnityEngine;
using Web3Helpers;

[CreateAssetMenu(fileName = "Fashion Outfit", menuName = "Macho/Wearables/Outfit", order = 1)]
public class CharacterPreset : InventoryItem {
    bool outfitSet = false;
    [SerializeField] CharacterLayerItem[] outfitLayers;
    [SerializeField] CharacterLayerLibrary fashionLibrary;

    public CharacterLayerLibrary FashionLibrary => fashionLibrary;
    public override bool IsUsable => true;

    public void SetCollection(CollectionIdentifier collection) {
        this.collection = collection;
    }

    public void SetOutfitLayers(CharacterLayerItem[] outfitLayers, CharacterLayerLibrary fashionLibrary) {
        if (!outfitSet) this.outfitLayers = outfitLayers;
        outfitSet = true;
        this.fashionLibrary = fashionLibrary;
    }

    public IEnumerable<CharacterLayerItem> GetOutfitLayers() {
        return outfitLayers;
    }

    public void EquipOutfit(CharacterLayerManager manager) {
        manager.WearOutfit(this);
    }

    protected override void UseItemMethod() {
        EquipOutfit(Tooltip.CharacterManager);
    }
}