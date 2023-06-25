using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using Newtonsoft.Json;
using UnityEngine;

public class SerializableEquipment : EquipmentManager {
    void Awake() {
        equipment = RetrieveDictionary();
    }

    private Dictionary<EquipmentType, EquippableItem> RetrieveDictionary() {
        if (!File.Exists(Application.persistentDataPath + "/equipment.json")) return new Dictionary<EquipmentType, EquippableItem>();
        string jsonString = File.ReadAllText(Application.persistentDataPath + "/equipment.json");
        Debug.Log("Equipment retrieved");
        var equipment = JsonConvert.DeserializeObject<Dictionary<EquipmentType, EquippableItem>>(jsonString);
        return equipment;
        /* if (!File.Exists(Application.persistentDataPath + "/equipment.equip")) return new Dictionary<EquipmentType, EquippableItem>();
        using (FileStream stream = new(Application.persistentDataPath + "/equipment.equip", FileMode.Open)) {
            BinaryFormatter bF = new();
            var equipment = bF.Deserialize(stream) as Dictionary<EquipmentType, EquippableItem>;
            Debug.Log("Equipment retrieved");
            return equipment;
        } */
    }

    private void SerializeDictionary() {
        string jsonString = JsonConvert.SerializeObject(equipment);
        Debug.Log(jsonString);
        File.WriteAllText(Application.persistentDataPath + "/equipment.json", jsonString);
        Debug.Log("Equipment JSONified");
        /* using (FileStream stream = new(Application.persistentDataPath + "/equipment.equip", FileMode.Create)) {
            BinaryFormatter bF = new();
            bF.Serialize(stream, equipment);
            Debug.Log("Equipment serialized");
        } */
    }

    void OnDestroy() {
        SerializeDictionary();
    }
}