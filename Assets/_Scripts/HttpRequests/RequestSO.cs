using UnityEngine;

[CreateAssetMenu(fileName = "RequestSO", menuName = "Macho/RequestSO", order = 0)]
public class RequestSO : ScriptableObject {
    [SerializeField] bool fetchSmols = true;

    private void OnValidate() {
        Debug.Log(DataFetcher.smols);
        if (fetchSmols && DataFetcher.smols == null) {
            DataFetcher.FetchSmols("0xbE8Caf82259D44EeCd0A6BcdB82655a4F6711b1A");
        }
        //LoopThroughSmols();
    }

    void LoopThroughSmols() {
        if (DataFetcher.smols != null) {
            foreach(Smol smol in DataFetcher.smols) Debug.Log(smol.tokenId);
        }
    }
}