using System.Threading.Tasks;
using UnityEngine;

public static class DataFetcher {
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

[System.Serializable]
public class Smol {
    public string tokenId { get; set; }
    public Attributes attributes { get; set; }
}

[System.Serializable]
public class Attributes {
    public string Background { get; set; }

    public string Body { get; set; }

    public string Clothes { get; set; }

    public string Glasses { get; set; }

    public string Hat { get; set; }

    public string Hair { get; set; }

    public string Mouth { get; set; }

    public string Gender { get; set; }
}