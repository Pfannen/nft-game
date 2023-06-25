using UnityEngine;
using UnityEngine.U2D.Animation;
using Web3Helpers;

[System.Serializable]
public class EquippableAttributes : EquippableItem
{
    [SerializeField] Attributes attributes;
    [SerializeField] SpriteLibraryAsset library;
    public Attributes Attributes => attributes;
    public SpriteLibraryAsset Library => library;

    public void SetAttributes(Attributes attributes, SpriteLibraryAsset library) {
        this.attributes = attributes;
        this.library = library;
        equipmentType = EquipmentType.Outfit;
    }
}