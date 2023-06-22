using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Web3Helpers {
    public class WearableOutfit : MonoBehaviour, IRaycastable {
        [SerializeField] OutfitSelectionMethod selectionMethod = OutfitSelectionMethod.Tooltip;
        private Attributes attributes;

        public Attributes Attributes => attributes;
        public static event Action<WearableOutfit> OutfitClicked; 

        public void SetAttributes(Attributes attributes) {
            this.attributes = attributes;
        }

        public virtual void OnRaycast() {
            if (selectionMethod == OutfitSelectionMethod.Tooltip) {
                OutfitClicked?.Invoke(this);
            } else {
                SpriteController.SelectedOutfit = attributes;
                SceneManager.LoadScene(1);
                Debug.Log(attributes.Body);
            }
        }
    }

    public enum OutfitSelectionMethod {
        Raycast,
        Tooltip
    }
}