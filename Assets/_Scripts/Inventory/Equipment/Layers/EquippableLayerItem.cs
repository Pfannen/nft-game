using UnityEngine;
using Web3Helpers;

[CreateAssetMenu(fileName = "Layer Item", menuName = "Macho/Wearables/Layer Item", order = 0)]
public class EquippableLayerItem : EquippableItem {
    [SerializeField] string layerName;
    [SerializeField] int layerOrder;

    public Sprite Sprite => image;
    public string SpriteName => itemName;
    public string LayerName => layerName;
    public int LayerOrder => layerOrder;
}