using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayCharacterButton : MonoBehaviour {
    [SerializeField] SerializableCharacterManager characterSetter;

    private Button button;
    private CharacterPreset selectedCharacter = null;

    void Start() {
        button = GetComponent<Button>();
        button.interactable = false;
        button.onClick.AddListener(OnClick);
    }

    void OnEnable() {
        CharacterBoxOutfit.CharacterSelected += SetButtonInteractable;
    }

    void OnDisable() {
        CharacterBoxOutfit.CharacterSelected -= SetButtonInteractable;
    }

    private void SetButtonInteractable(CharacterPreset outfit) {
        button.interactable = true;
        selectedCharacter = outfit;
    }

    private void OnClick() {
        characterSetter.WearOutfit(selectedCharacter);
        SceneManager.LoadScene(1);
    }
}