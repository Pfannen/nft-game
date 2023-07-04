using System;
using UnityEngine;

public class CharacterBoxOutfit : MonoBehaviour, IRaycastable {
    public static event Action<CharacterPreset> CharacterSelected;

    public CharacterPreset Outfit;

    public void OnRaycast() {
        CharacterSelected?.Invoke(Outfit);
    }
}