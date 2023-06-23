using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SerializableEquipment : EquipmentManager {
    void Awake() {
        equipment = RetrieveDictionary();
    }

    private Dictionary<EquipmentType, object> RetrieveDictionary() {
        if (!File.Exists(Application.persistentDataPath + "/equipment.equip")) return new Dictionary<EquipmentType, object>();
        using (FileStream stream = new(Application.persistentDataPath + "/equipment.equip", FileMode.Open)) {
            BinaryFormatter bF = new();
            var equipment = bF.Deserialize(stream) as Dictionary<EquipmentType, object>;
            Debug.Log("Equipment retrieved");
            return equipment;
        }
    }

    private void SerializeDictionary() {
        using (FileStream stream = new(Application.persistentDataPath + "/equipment.equip", FileMode.Create)) {
            BinaryFormatter bF = new();
            bF.Serialize(stream, equipment);
            Debug.Log("Equipment serialized");
        }
    }

    void OnDestroy() {
        SerializeDictionary();

    }
}