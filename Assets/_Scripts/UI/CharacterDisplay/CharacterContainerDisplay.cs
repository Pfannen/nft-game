using UnityEngine;
using Web3Helpers;

public class CharacterContainerDisplay : MonoBehaviour {
    [SerializeField] GameObject characterContainer;
    [SerializeField] CharacterBoxOutfit characterBoxPrefab;
    [SerializeField] FashionLibrary mFL;
    [SerializeField] FashionLibrary fFL;

    void Start() {
        FillContainer();
    }

    private async void FillContainer() {
        var characters = await CharacterInventory.Instance?.GetCharacters();
        foreach (var pair in characters) {
            if (pair.Key == CollectionIdentifier.Smol) {
                Smol smol = (Smol)pair.Value;
                FashionOutfit smolOutfit = ImageBuilder.BuildOutfitFromSmol(smol, smol.attributes.Gender == "female" ? fFL : mFL);
                var parentContainer = Instantiate(characterBoxPrefab, new Vector3(0,0,0), Quaternion.identity, characterContainer.transform);
                parentContainer.Outfit = smolOutfit;
                ImageBuilder.BuildImageLayersFromOutfit(smolOutfit, parentContainer.GetComponent<RectTransform>(), true);
            }
        }
    }
}