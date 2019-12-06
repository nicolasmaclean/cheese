using UnityEngine;

public class UnitSoldier : Unit
{
    public override void initialize(Vector2 gPos)
    {
        maxHealth = 150;
        moveRange = 2;
        damage = 50;
        levelMult = 1.2;
        entityName = "Soldier Ant";
        
        base.initialize(gPos);
    }

    public override void promoteToVeteran()
    {
        gameObject.GetComponent<Renderer>().sharedMaterial = Resources.Load<Material>("Materials/SkinGod");

        base.promoteToVeteran();
    }
}