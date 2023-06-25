using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "InventoryItem", menuName = "Macho/Item", order = 0)]
public class InventoryItem : ScriptableObject {
    [SerializeField] protected Sprite image;
    [SerializeField] protected string itemName;
    [SerializeField] protected string description;
    [SerializeField] protected int amount;
    [SerializeField] protected int? tokenId;

    public Sprite Image => image;
    public string ItemName => itemName;
    public string Description => description;
    public int Amount => amount;

    public void Initialize(Sprite image, string itemName, string description, int amount, int? tokenId) {
        this.image = image;
        this.itemName = itemName;
        this.description = description;
        this.amount = amount;
        this.tokenId = tokenId;
    }

    public void SetAmount(int amount) {
        this.amount = amount;
    }
}