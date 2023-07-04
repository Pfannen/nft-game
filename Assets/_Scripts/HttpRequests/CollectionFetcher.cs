using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using UnityEngine;
using Web3Helpers;

public static class CollectionFetcher {
    public static string TestingAddress = "0xbE8Caf82259D44EeCd0A6BcdB82655a4F6711b1A";

    public static Smol[] Smols { get; private set; } = null;

    public static async Task FetchSmols(string address) {
        try {
            Smols = await HttpRequest.Get<Smol[]>($"http://localhost:3000/metadata/smol?account={address}");
            Debug.Log(Smols.Length);
        } catch (System.Exception ex) {
            Debug.Log(ex.Message);
        }
    }

    public static void SetSmols(Smol[] smols) {
        Smols = smols;
    }

    public static async Task<Smol[]> GetSmols(string address) {
        if (Smols != null) return Smols;
        string path = Application.persistentDataPath + "/Smols.smol";
        if (File.Exists(path)) {
            using (FileStream stream = new(path, FileMode.Open)) {
                BinaryFormatter bF = new();
                Smol[] smols = bF.Deserialize(stream) as Smol[];
                Smols = smols;
                Debug.Log("Read");
                return Smols;
            }
        } else {
            await FetchSmols(address);
            using (FileStream stream = new(path, FileMode.Create)) {
                BinaryFormatter bF = new();
                bF.Serialize(stream, CollectionFetcher.Smols);
            }
            return Smols;
        }
    }
}