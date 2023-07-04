using System;
using System.Collections.Generic;
using UnityEngine;
using Web3Helpers;

[CreateAssetMenu(fileName = "Fashion Library", menuName = "Macho/Wearables/Fashion Library", order = 0)]
public class CharacterLayerLibrary : ScriptableObject {
    [SerializeField] CollectionIdentifier collection;
    [SerializeField] ItemLayer[] layers;
    [SerializeField] Dictionary<string, Dictionary<string, CharacterLayerItem>> layerToItem = null;

    public CollectionIdentifier Collection => collection;

    public CharacterLayerItem GetLayerItem(string layerName, string layerItemName) {
        if (layerToItem.TryGetValue(layerName, out Dictionary<string, CharacterLayerItem> items)) {
            if (items.TryGetValue(layerItemName, out CharacterLayerItem item)) return item;
        }
        return null;
    }

//#if UNITY_EDITOR
    void OnEnable() {
        if (layerToItem != null) return;
        layerToItem = new Dictionary<string, Dictionary<string, CharacterLayerItem>>();
        for (int i = 0; i < layers.Length; i++) {
            ItemLayer layer = layers[i];
            layerToItem.TryAdd(layer.name, new Dictionary<string, CharacterLayerItem>());
            foreach (var item in layer.items) {
                layerToItem[layer.name].Add(item.ItemName, item);
            }
        }
    }
//#endif

    [Serializable]
    struct ItemLayer {
        public string name;
        public CharacterLayerItem[] items;
    }
}