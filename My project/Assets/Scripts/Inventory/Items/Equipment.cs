using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/Equipment")]
public class Equipment : Item {

    public EquipmentSlot equipSlot;
    public SkinnedMeshRenderer mesh;
    public EquipmentMeshRegion[] coveredMeshRegions;
    
    public int armorModifier;
    public int demageModifier;

    public override void Use() {
        
        base.Use();

        // Equip the item
        EquipmentManager.instance.Equip(this);
        // Remove it fromthe inventory
        RemoveFromInventory();
    }
}

public enum EquipmentSlot { Head, Chest, Gloves, Legs, Feet, Weapon, Shield }
public enum EquipmentMeshRegion {Head, Chest, Arms, Legs, Feet}; // corresponds to body blendshapes.
