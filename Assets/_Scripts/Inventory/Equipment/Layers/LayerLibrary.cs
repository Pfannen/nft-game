using System;
using System.Collections.Generic;
using UnityEngine;
using Web3Helpers;

[CreateAssetMenu(fileName = "Layer Library", menuName = "Macho/Wearables/Layer Library", order = 0)]
public class LayerLibrary : ScriptableObject {
    [SerializeField] CollectionIdentifier collection;
    [SerializeField] ItemLayer[] layers;
    Dictionary<string, Dictionary<string, EquippableLayerItem>> layerToItem = new Dictionary<string, Dictionary<string, EquippableLayerItem>>();

    public EquippableLayerItem GetLayerItem(string layerName, string layerItemName) {
        return layerToItem[layerName][layerItemName];
    }

#if UNITY_EDITOR
    void OnValidate() {
        layerToItem = new Dictionary<string, Dictionary<string, EquippableLayerItem>>();
        for (int i = 0; i < layers.Length; i++) {
            ItemLayer layer = layers[i];
            layerToItem.TryAdd(layer.name, new Dictionary<string, EquippableLayerItem>());
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
        public EquippableLayerItem[] items;
    }
}