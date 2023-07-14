using System;
using UnityEngine;
using Web3Helpers;

public class CharacterLayerManager : MonoBehaviour {
    protected CharacterLayerItem[] wearables;
    protected CharacterLayerLibrary wearablesLibrary;
    protected CollectionIdentifier wearableCollection = CollectionIdentifier.Smol;
    protected CharacterPreset wearableOutfit;

    public event Action<int> ItemWorn;
    public event Action<int> ItemRemoved;
    public CollectionIdentifier WearableCollection => wearableCollection;

    protected virtual void Awake() {
        wearables = new CharacterLayerItem[LayerHelper.NumLayers(wearableCollection)];
    }

    public bool WearOutfit(CharacterPreset outfit) {
        if (outfit.Collection != wearableCollection) return false;
        for (int i = 0; i < wearables.Length; i++) RemoveItem(i);
        foreach (var item in outfit.GetOutfitLayers()) WearItem(item);
        wearableOutfit = outfit;
        wearablesLibrary = outfit.FashionLibrary;
        return true;
    }

    public bool WearItem(CharacterLayerItem item) {
        if (item == null) return true;
        if (item.Collection != wearableCollection) return false;
        wearables[item.LayerOrder] = item;
        ItemWorn?.Invoke(item.LayerOrder);
        return true;
    }

    public void RemoveItem(int order) {
        wearables[order] = null;
        ItemRemoved?.Invoke(order);
        wearableOutfit = null;
    }

    public CharacterLayerItem GetItem(int order) {
        return wearables[order];
    }
}