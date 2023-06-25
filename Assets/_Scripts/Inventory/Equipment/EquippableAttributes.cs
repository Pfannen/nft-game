using UnityEngine;
using UnityEngine.U2D.Animation;
using Web3Helpers;

[System.Serializable]
public class EquippableAttributes : EquippableItem
{
    [SerializeField] Attributes attributes;
    [SerializeField] SpriteLibraryAsset library;
    [SerializeField] Outfit outfit;
    public Attributes Attributes => attributes;
    public SpriteLibraryAsset Library => library;

    public void SetAttributes(Attributes attributes, SpriteLibraryAsset library) {
        this.attributes = attributes;
        this.library = library;
        outfit = new Outfit(attributes);
        equipmentType = EquipmentType.Outfit;
    }

    void OnValidate() {
        attributes = new Attributes();
        attributes.Background = outfit.Background;
        attributes.Body = outfit.Body;
        attributes.Clothes = outfit.Clothes;
        attributes.Glasses = outfit.Glasses;
        attributes.Hat = outfit.Hat;
        attributes.Hair = outfit.Hair;
        attributes.Mouth = outfit.Mouth;
        attributes.Gender = outfit.Gender;
    }
}
[System.Serializable]
    public class Outfit {
        public string Background;

        public string Body;

        public string Clothes;

        public string Glasses;

        public string Hat;

        public string Hair;

        public string Mouth;

        public string Gender;

        public Outfit(Attributes attributes) {
            Background = attributes.Background;
            Body = attributes.Body;
            Clothes = attributes.Clothes;
            Glasses = attributes.Glasses;
            Hat = attributes.Hat;
            Hair = attributes.Hair;
            Mouth = attributes.Mouth;
            Gender = attributes.Gender;
        }
    }