using UnityEngine;

public class CharacterDisplay : MonoBehaviour {
    void OnEnable() {
        CharacterBoxOutfit.CharacterSelected += DisplayCharacter;
    }

    void OnDisable() {
        CharacterBoxOutfit.CharacterSelected -= DisplayCharacter;
    }

    private void DisplayCharacter(FashionOutfit outfit) {
        ImageBuilder.BuildImageLayersFromOutfit(outfit, GetComponent<RectTransform>(), true);
    }
}