using System;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.Networking;

public static class HttpRequest {
    public static async Task<T> Get<T>(string url) {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url)) {
            var operation = webRequest.SendWebRequest();
            while (!operation.isDone) await Task.Yield();
            if (webRequest.result != UnityWebRequest.Result.Success) throw new System.Exception(webRequest.error);
            var res = webRequest.downloadHandler.text;
            var settings = new JsonSerializerSettings();
            return JsonConvert.DeserializeObject<T>(res);
        }
    }

    public static async Task Post(string url, System.Object data) {
        using (UnityWebRequest webRequest = UnityWebRequest.Post(url, "")) {
            webRequest.SetRequestHeader("Content-Type", "application/json");
            webRequest.uploadHandler = new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(JsonUtility.ToJson(data)));
            var operation = webRequest.SendWebRequest();
            while (!operation.isDone) await Task.Yield();
            if (webRequest.result != UnityWebRequest.Result.Success) throw new System.Exception(webRequest.error);
        }
    }
}
