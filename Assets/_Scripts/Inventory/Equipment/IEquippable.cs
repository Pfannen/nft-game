public interface IEquippable {
    public void Equip(EquipmentManager equipment);

    public bool IsEquipped(EquipmentManager equipment);

    public object ItemToEquip();
}