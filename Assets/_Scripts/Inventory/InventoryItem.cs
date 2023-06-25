using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "InventoryItem", menuName = "Macho/Item", order = 0)]
public class InventoryItem : ScriptableObject {
    [SerializeField] protected Sprite image;
    [SerializeField] protected string itemName;
    [SerializeField] protected string description;
    [SerializeField] protected string collectionName;
    [SerializeField] protected int tokenId;

    public Sprite Image => image;
    public string ItemName => itemName;
    public string Description => description;
    public string CollectionName => collectionName;
    public int TokenId => tokenId;

    public void Initialize(Sprite image, string itemName, string description, string collectionName, int tokenId) {
        this.image = image;
        this.itemName = itemName;
        this.description = description;
        this.collectionName = collectionName;
        this.tokenId = tokenId;
    }
}