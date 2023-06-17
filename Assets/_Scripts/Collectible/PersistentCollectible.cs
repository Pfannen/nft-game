using UnityEngine;

public abstract class PersistentCollectible : MonoBehaviour {
    [SerializeField] int collectibleId;

    public delegate void Collect(int collectibleId, int amount);
    public static event Collect OnCollect;

    protected void Collected(int amount) {
        OnCollect?.Invoke(collectibleId, amount);
    }
}