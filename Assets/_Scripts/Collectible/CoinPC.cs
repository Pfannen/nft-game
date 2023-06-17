using UnityEngine;

public class CoinPC : PersistentCollectible {
    [SerializeField] int amount = 1;

    void OnTriggerEnter2D(Collider2D collider) {
        Destroy(gameObject);
        Collected(amount);
    }
}