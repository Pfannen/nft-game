using UnityEngine;

public class CharacterInventoryDisplayer : MonoBehaviour {
    SpriteController playerSC;

    void Awake() {
        if (!playerSC) playerSC = FindObjectOfType<SerializableCharacterManager>().gameObject.GetComponent<SpriteController>();
        if (playerSC && transform.childCount == 0) {
            var children = Instantiate(playerSC.SpritesHolder, Vector3.zero, Quaternion.identity);
            children.transform.SetParent(transform, false);
        } else Debug.Log("No playerSC");
    }
}