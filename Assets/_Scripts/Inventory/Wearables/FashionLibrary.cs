using System;
using System.Collections.Generic;
using UnityEngine;
using Web3Helpers;

[CreateAssetMenu(fileName = "Fashion Library", menuName = "Macho/Wearables/Fashion Library", order = 0)]
public class FashionLibrary : ScriptableObject {
    [SerializeField] CollectionIdentifier collection;
    [SerializeField] ItemLayer[] layers;
    Dictionary<string, Dictionary<string, FashionItem>> layerToItem = new Dictionary<string, Dictionary<string, FashionItem>>();

    public FashionItem GetLayerItem(string layerName, string layerItemName) {
        return layerToItem[layerName][layerItemName];
    }

#if UNITY_EDITOR
    void OnValidate() {
        layerToItem = new Dictionary<string, Dictionary<string, FashionItem>>();
        for (int i = 0; i < layers.Length; i++) {
            ItemLayer layer = layers[i];
            layerToItem.TryAdd(layer.name, new Dictionary<string, FashionItem>());
            foreach (var item in layer.items) {
                layerToItem[layer.name].Add(item.ItemName, item);
            }
        }
        Debug.Log(layerToItem.Count);
    }
#endif

    [Serializable]
    struct ItemLayer {
        public string name;
        public FashionItem[] items;
    }
}