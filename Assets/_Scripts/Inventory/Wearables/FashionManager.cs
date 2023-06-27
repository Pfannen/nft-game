using System;
using UnityEngine;
using Web3Helpers;

public class FashionManager : MonoBehaviour {
    protected FashionItem[] wearables;
    protected FashionLibrary wearablesLibrary;
    protected CollectionIdentifier wearableCollection = CollectionIdentifier.Smol;
    protected FashionOutfit wearableOutfit;

    public event Action<FashionItem> ItemWorn;
    public event Action<int> ItemRemoved;
    public CollectionIdentifier WearableCollection => wearableCollection;

    void Awake() {
        wearables = new FashionItem[LayerHelper.NumLayers(wearableCollection)];
    }

    public bool WearOutfit(FashionOutfit outfit) {
        if (outfit.Collection != wearableCollection) return false;
        foreach (var item in outfit.GetOutfitLayers()) WearItem(item);
        wearableOutfit = outfit;
        return true;
    }

    public bool WearItem(FashionItem item) {
        if (item.Collection != wearableCollection) return false;
        wearables[item.LayerOrder] = item;
        ItemWorn?.Invoke(item);
        return true;
    }

    public void RemoveItem(int order) {
        wearables[order] = null;
        ItemRemoved?.Invoke(order);
        wearableOutfit = null;
    }

    public FashionItem GetItem(int order) {
        return wearables[order];
    }
}