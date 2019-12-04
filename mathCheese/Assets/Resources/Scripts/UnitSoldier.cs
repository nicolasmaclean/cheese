using UnityEngine;

public class UnitSoldier : Unit
{
    public bool veteran = false;
    public override void initialize(Vector2 gPos)
    {
        maxHealth = 150;
        moveRange = 2;
        damage = 50;
        entityName = "Soldier Ant";
        
        base.initialize(gPos);
    }

    public void promoteToVeteran()
    {
        maxHealth = 200;
        damage = 75;
        gameObject.GetComponent<Renderer>().sharedMaterial = Resources.Load<Material>("Materials/SkinGod");
        veteran = true;
    }
}