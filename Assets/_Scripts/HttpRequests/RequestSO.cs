using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using Web3Helpers;

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
        Debug.Log(CollectionFetcher.Smols?[0].tokenId);
        if (File.Exists(path + "/Smols.smol")) {
            if (CollectionFetcher.Smols == null) ReadSmols();
        }
        else FetchAndSerializeSmols();
        //LoopThroughSmols();
    }

    private async void FetchAndSerializeSmols() {
        await CollectionFetcher.FetchSmols("0xbE8Caf82259D44EeCd0A6BcdB82655a4F6711b1A");
        SerializeSmols();
    }

    void LoopThroughSmols() {
        if (CollectionFetcher.Smols != null) {
            foreach(Smol smol in CollectionFetcher.Smols) Debug.Log(smol.tokenId);
        }
    }

    public void ReadSmols() {
        using (FileStream stream = new(path + "/Smols.smol", FileMode.Open)) {
            BinaryFormatter bF = new();
            Smol[] smols = bF.Deserialize(stream) as Smol[];
            CollectionFetcher.SetSmols(smols);
            Debug.Log("Read");
            OnSetSmol?.Invoke(CollectionFetcher.Smols[0]);
        }
    }

    private void SerializeSmols() {
        using (FileStream stream = new(path + "/Smols.smol", FileMode.Create)) {
            BinaryFormatter bF = new();
            bF.Serialize(stream, CollectionFetcher.Smols);
        }
    }
}