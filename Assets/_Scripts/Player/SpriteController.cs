using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;
using Web3Helpers;

public class SpriteController : MonoBehaviour {
    private static Attributes defaultAttributes = new Attributes();
    public static Attributes SelectedOutfit { get; set; } = defaultAttributes;

    [SerializeField] RequestSO request;
    SpriteResolver[] resolvers;
    int curFit = 0;

    void Start() {
        resolvers = GetComponentsInChildren<SpriteResolver>();
        SetOutfit(SelectedOutfit);
    }

    void Update() {
        if (Input.GetKeyDown("l")) {
            curFit++;
            if (curFit >= 6) curFit = 0;
            SetOutfit(CollectionFetcher.Smols[curFit].attributes);
        }
    }

    public void SetOutfit(Attributes attributes) {
        if (attributes == null) return;
        resolvers[0].SetCategoryAndLabel("Hat", attributes.Hat);
        resolvers[1].SetCategoryAndLabel("Eye", attributes.Glasses);
        resolvers[2].SetCategoryAndLabel("Body", attributes.Body);
        resolvers[3].SetCategoryAndLabel("Mouth", attributes.Mouth);
        resolvers[4].SetCategoryAndLabel("Shirt", attributes.Clothes);
    }
}