using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

[CreateAssetMenu(fileName = "RequestSO", menuName = "Macho/RequestSO", order = 0)]
public class RequestSO : ScriptableObject {
    [SerializeField] bool fetchSmols = true;
    string path = null;

    public event Action<Smol> OnSetSmol;

    void OnEnable() {
        path = Application.persistentDataPath;
    }

    private void OnValidate() {
        if (path == null) return;
        Debug.Log(DataFetcher.Smols?[0].tokenId);
        if (File.Exists(path + "/Smols.smol")) {
            if (DataFetcher.Smols == null) ReadSmols();
        }
        else FetchAndSerializeSmols();
        //LoopThroughSmols();
    }

    async void FetchAndSerializeSmols() {
        await DataFetcher.FetchSmols("0xbE8Caf82259D44EeCd0A6BcdB82655a4F6711b1A");
        SerializeSmols();
    }

    void LoopThroughSmols() {
        if (DataFetcher.Smols != null) {
            foreach(Smol smol in DataFetcher.Smols) Debug.Log(smol.tokenId);
        }
    }

    public void ReadSmols() {
        using (FileStream stream = new(path + "/Smols.smol", FileMode.Open)) {
            BinaryFormatter bF = new();
            Smol[] smols = bF.Deserialize(stream) as Smol[];
            DataFetcher.SetSmols(smols);
            Debug.Log("Read");
            OnSetSmol?.Invoke(DataFetcher.Smols[0]);
        }
    }

    void SerializeSmols() {
        using (FileStream stream = new(path + "/Smols.smol", FileMode.Create)) {
            BinaryFormatter bF = new();
            bF.Serialize(stream, DataFetcher.Smols);
        }
    }
}