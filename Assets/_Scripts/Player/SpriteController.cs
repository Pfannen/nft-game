using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;
using Web3Helpers;

public class SpriteController : MonoBehaviour {
    private static Attributes defaultAttributes = new Attributes();

    [SerializeField] RequestSO request;
    [SerializeField] SpriteLibrary library;
    SpriteResolver[] resolvers;
    EquipmentManager equipmentManager;
    int curFit = 0;

    void Awake() {
        equipmentManager = GetComponent<EquipmentManager>();
        resolvers = GetComponentsInChildren<SpriteResolver>();
    }

    void Start() {
        var outfit = equipmentManager.GetEquipment(EquipmentType.Outfit) as EquippableAttributes;
        if (outfit == null) SetOutfit(defaultAttributes, null);
        else SetOutfit(outfit.Attributes, outfit.Library);
    }

    void Update() {
        if (Input.GetKeyDown("l")) {
            curFit++;
            if (curFit >= 6) curFit = 0;
            SetOutfit(CollectionFetcher.Smols[curFit].attributes, null);
        }
    }

    public void SetOutfit(Attributes attributes, SpriteLibraryAsset libraryAsset) {
        if (attributes == null) return;
        if (libraryAsset != null) library.spriteLibraryAsset = libraryAsset;
        resolvers[0].SetCategoryAndLabel("Hat", attributes.Hat);
        resolvers[1].SetCategoryAndLabel("Glasses", attributes.Glasses);
        resolvers[2].SetCategoryAndLabel("Body", attributes.Body);
        resolvers[3].SetCategoryAndLabel("Mouth", attributes.Mouth);
        resolvers[4].SetCategoryAndLabel("Clothes", attributes.Clothes);
    }
}