using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using Web3Helpers;

public class SerializableFashion : FashionManager {
    protected override void Awake() {
        base.Awake();
        RetrieveWearables();
    }

    private void RetrieveWearables() {
        if (!File.Exists(Application.persistentDataPath + "/wearables.wear")) return;
        using (FileStream stream = new(Application.persistentDataPath + "/wearables.wear", FileMode.Open)) {
            BinaryFormatter bF = new();
            string[] itemIdentifiers = bF.Deserialize(stream) as string[];
            wearablesLibrary = Resources.Load<FashionLibrary>($"Wearables/{itemIdentifiers[0]}");
            wearableCollection = wearablesLibrary.Collection;
            wearables = new FashionItem[LayerHelper.NumLayers(wearablesLibrary.Collection)];
            for (int i = 1; i < itemIdentifiers.Length; i++) {
                if (itemIdentifiers[i] != null) {
                    string[] pathInLib = itemIdentifiers[i].Split("/");
                    Debug.Log(pathInLib[1]);
                    wearables[i - 1] = wearablesLibrary.GetLayerItem(pathInLib[0], pathInLib[1]);
                }
            }
        }
    }

    private void SerializeWearables() {
        using (FileStream stream = new(Application.persistentDataPath + "/wearables.wear", FileMode.Create)) {
            BinaryFormatter bF = new();
            string[] itemIdentifiers = new string[wearables.Length + 1];
            itemIdentifiers[0] = wearablesLibrary.name;
            for (int i = 1; i < wearables.Length + 1; i++) {
                var wearable = wearables[i - 1];
                if (wearable != null) itemIdentifiers[i] = $"{wearable.LayerName}/{wearable.ItemName}";
            }
            bF.Serialize(stream, itemIdentifiers);
        }
    }

    void OnDestroy() {
        SerializeWearables();
    }
}