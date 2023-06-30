using UnityEngine;

[CreateAssetMenu(fileName = "Mount", menuName = "Macho/Mount", order = 0)]
public class Mount : EquippableItem {
    [SerializeField] GameObject prefab;
    [SerializeField] float mountSpeed;

    public GameObject Prefab => prefab;
    public float MountSpeed => mountSpeed;
}