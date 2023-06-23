using UnityEngine.U2D.Animation;
using Web3Helpers;

public class SpriteBuilderTokenUI : InventoryItemUI, IRaycastable, IEquippable {
    private SpriteLibraryAsset library;
    private Attributes attributes;

    public void InitializeAttributes(SpriteLibraryAsset library, Attributes attributes) {
        this.library = library;
        this.attributes = attributes;
        ImageBuilder.SetImages(this.gameObject, attributes, library);
    }

    public void OnRaycast() {
        OnInventoryItemSelected(this);
    }

    public void Equip() {
        SpriteController.SelectedOutfit = attributes;
    }

    public bool IsEquipped() {
        return SpriteController.SelectedOutfit == attributes;
    }
}