using UnityEngine;

public class CharacterDisplay : MonoBehaviour {
    void OnEnable() {
        CharacterBoxOutfit.CharacterSelected += DisplayCharacter;
    }

    void OnDisable() {
        CharacterBoxOutfit.CharacterSelected -= DisplayCharacter;
    }

    private void DisplayCharacter(CharacterPreset outfit) {
        ImageBuilder.BuildImageLayersFromOutfit(outfit, GetComponent<RectTransform>(), true);
    }
}