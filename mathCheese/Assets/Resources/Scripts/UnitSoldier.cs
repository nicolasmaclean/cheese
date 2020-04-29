using UnityEngine;

public class UnitSoldier : Unit
{
    public override void initialize(Vector2 gPos, Material teamMaterial)
    {
        maxHealth = 150;
        moveRange = 2;
        damage = 50;
        levelMult = 1.2;
        entityName = "Soldier Ant";
        teamMaterialIndex = 3;
        
        base.initialize(gPos, teamMaterial);
    }

    public override void promoteToVeteran()
    {
        gameObject.GetComponent<Renderer>().sharedMaterial = Resources.Load<Material>("Materials/SkinGod");

        base.promoteToVeteran();
    }
}