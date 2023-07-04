using UnityEngine;
using UnityEngine.UI;
using Web3Helpers;
using TMPro;

public class CharacterContainerDisplay : MonoBehaviour {
    [SerializeField] GameObject characterContainer;
    [SerializeField] CharacterBoxOutfit characterBoxPrefab;
    [SerializeField] CharacterLayerLibrary mFL;
    [SerializeField] CharacterLayerLibrary fFL;

    void Start() {
        FillContainer();
    }

    private async void FillContainer() {
        var characters = await CharacterInventory.Instance?.GetCharacters();
        foreach (var pair in characters) {
            if (pair.Key == CollectionIdentifier.Smol) {
                Smol smol = (Smol)pair.Value;
                CharacterPreset smolOutfit = ImageBuilder.BuildCharacterFromSmol(smol, smol.attributes.Gender == "female" ? fFL : mFL);
                var parentContainer = Instantiate(characterBoxPrefab, new Vector3(0,0,0), Quaternion.identity, characterContainer.transform);
                parentContainer.Outfit = smolOutfit;
                ImageBuilder.BuildImageLayersFromOutfit(smolOutfit, parentContainer.GetComponent<RectTransform>(), true);
            }
        }
    }
}