using System;
using UnityEngine;
using UnityEngine.Events;
using Web3Helpers;

[System.Serializable]
[CreateAssetMenu(fileName = "InventoryItem", menuName = "Macho/Item", order = 0)]
public class InventoryItem : ScriptableObject {
    [SerializeField] protected Sprite image;
    [SerializeField] protected string itemName;
    [SerializeField] protected string description;
    [SerializeField] protected CollectionIdentifier collection;
    [SerializeField] protected int tokenId;

    public Sprite Image => image;
    public string ItemName => itemName;
    public string Description => description;
    public CollectionIdentifier Collection => collection;
    public int TokenId => tokenId;
    public virtual bool UseTooltipButton => false;
    public UnityAction TooltipButtonMethod => TooltipMethod;

    public void Initialize(Sprite image, string itemName, string description, CollectionIdentifier collection, int tokenId) {
        this.image = image;
        this.itemName = itemName;
        this.description = description;
        this.collection = collection;
        this.tokenId = tokenId;
    }

    protected virtual void TooltipMethod() { }
}