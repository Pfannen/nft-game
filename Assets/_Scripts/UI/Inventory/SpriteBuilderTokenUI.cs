using UnityEngine;
using UnityEngine.U2D.Animation;
using Web3Helpers;

public class SpriteBuilderTokenUI : InventoryItemUI, IRaycastable {
    private SpriteLibraryAsset library;
    private Attributes attributes;

    public override GameObject GetItemDisplay() {
        return gameObject;
    }

    public void InitializeAttributes(SpriteLibraryAsset library, Attributes attributes) {
        this.library = library;
        this.attributes = attributes;
        ImageBuilder.SetImages(this.gameObject, attributes, library);
    }

    public void OnRaycast() {
        OnInventoryItemSelected(this);
    }
}