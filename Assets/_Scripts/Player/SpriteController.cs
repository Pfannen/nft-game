using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;

public class SpriteController : MonoBehaviour {
    [SerializeField] RequestSO request;
    SpriteResolver[] resolvers;
    int curFit = 0;

    void Start() {
        resolvers = GetComponentsInChildren<SpriteResolver>();
        SetSmolOutfit(DataFetcher.Smols?[curFit]);
        request.OnSetSmol += SetSmolOutfit;
    }

    void Update() {
        if (Input.GetKeyDown("l")) {
            curFit++;
            if (curFit >= 6) curFit = 0;
            SetSmolOutfit(DataFetcher.Smols[curFit]);
        }
    }

    public void SetSmolOutfit(Smol smol) {
        if (smol == null) return;
        Attributes attributes = smol.attributes;
        resolvers[0].SetCategoryAndLabel("Hat", attributes.Hat);
        resolvers[1].SetCategoryAndLabel("Eye", attributes.Glasses);
        resolvers[2].SetCategoryAndLabel("Body", attributes.Body);
        resolvers[3].SetCategoryAndLabel("Mouth", attributes.Mouth);
        resolvers[4].SetCategoryAndLabel("Shirt", attributes.Clothes);
    }
}