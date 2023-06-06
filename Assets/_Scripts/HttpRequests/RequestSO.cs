using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

[CreateAssetMenu(fileName = "RequestSO", menuName = "Macho/RequestSO", order = 0)]
public class RequestSO : ScriptableObject {
    [SerializeField] bool fetchSmols = true;
    string path;

    public event Action<Smol> OnSetSmol;

    void OnEnable() {
        path = Application.persistentDataPath;
    }

    private void OnValidate() {
        Debug.Log(DataFetcher.smols?[0].tokenId);
        if (File.Exists(path + "/Smols.smol")) {
            if (DataFetcher.smols == null) ReadSmols();
        }
        else FetchAndSerializeSmols();
        //LoopThroughSmols();
    }

    async void FetchAndSerializeSmols() {
        await DataFetcher.FetchSmols("0xbE8Caf82259D44EeCd0A6BcdB82655a4F6711b1A");
        SerializeSmols();
    }

    void LoopThroughSmols() {
        if (DataFetcher.smols != null) {
            foreach(Smol smol in DataFetcher.smols) Debug.Log(smol.tokenId);
        }
    }

    public void ReadSmols() {
        using (FileStream stream = new(path + "/Smols.smol", FileMode.Open)) {
            BinaryFormatter bF = new();
            Smol[] smols = bF.Deserialize(stream) as Smol[];
            DataFetcher.smols = smols;
            Debug.Log("Read");
            OnSetSmol?.Invoke(DataFetcher.smols[0]);
        }
    }

    void SerializeSmols() {
        using (FileStream stream = new(path + "/Smols.smol", FileMode.Create)) {
            BinaryFormatter bF = new();
            bF.Serialize(stream, DataFetcher.smols);
        }
    }
}