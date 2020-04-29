using UnityEngine;

public class UnitGod : Unit
{
    public override void initialize(Vector2 gPos, Material teamMaterial)
    {
        maxHealth = 10000;
        moveRange = 100;
        damage = 500;
        entityName = "God Ant";
        teamMaterialIndex = 2;
        
        base.initialize(gPos, teamMaterial);
    }
}