using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using Newtonsoft.Json;
using UnityEngine;

public class SerializableEquipment : EquipmentManager {
    void Awake() {
        RetrieveDictionary();
    }

    private void RetrieveDictionary() {
        if (!File.Exists(Application.persistentDataPath + "/equipment.equip")) return;
        using (FileStream stream = new(Application.persistentDataPath + "/equipment.equip", FileMode.Open)) {
            BinaryFormatter bF = new();
            var equipmentToFilePath = bF.Deserialize(stream) as Dictionary<EquipmentType, string>;
            foreach (var pair in equipmentToFilePath) {
                EquippableItem item = Resources.Load<EquippableItem>(pair.Value);
                SetEquipment(pair.Key, item);
            }
        }
    }

    private void SerializeDictionary() {
        using (FileStream stream = new(Application.persistentDataPath + "/equipment.equip", FileMode.Create)) {
            BinaryFormatter bF = new();
            var equipmentToFilePath = new Dictionary<EquipmentType, string>();
            foreach (var pair in equipment) {
                equipmentToFilePath.Add(pair.Key, $"Inventory/{pair.Value.CollectionName}/{pair.Value.name}");
            }
            bF.Serialize(stream, equipmentToFilePath);
        }
    }

    void OnDestroy() {
        SerializeDictionary();
    }
}