using System;
using UnityEngine;

public class CharacterBoxOutfit : MonoBehaviour, IRaycastable {
    public static event Action<FashionOutfit> CharacterSelected;

    public FashionOutfit Outfit;

    public void OnRaycast() {
        CharacterSelected?.Invoke(Outfit);
    }
}