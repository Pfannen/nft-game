using UnityEngine;
using Web3Helpers;

[System.Serializable]
public class EquippableAttributes : EquippableItem
{
    [SerializeField] Attributes attributes;
    public Attributes Attributes => attributes;

    public void SetAttributes(Attributes attributes) {
        this.attributes = attributes;
    }
}