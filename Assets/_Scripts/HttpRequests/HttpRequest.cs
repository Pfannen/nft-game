using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine.Networking;

public static class HttpRequest {
    public static async Task<T> Get<T>(string url) {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url)) {
            var operation = webRequest.SendWebRequest();
            while (!operation.isDone) await Task.Yield();
            if (webRequest.result != UnityWebRequest.Result.Success) throw new System.Exception(webRequest.error);
            var res = webRequest.downloadHandler.text;
            return JsonConvert.DeserializeObject<T>(res);
        }
    }
}
