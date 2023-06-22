using System.Threading.Tasks;
using UnityEngine;
using Web3Helpers;

public static class CollectionFetcher {
    public static Smol[] Smols { get; private set; }

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
}