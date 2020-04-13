using System.Collections.Generic;
using UnityEngine;

public class Tile : Entity
{
    public bool isInMoveRange = false;
    public Material defaultMaterial;
    public int gold;
    public int water;
    public int food;
    public Transform groundT;

    public override void initialize(Vector2 gPos)
    {
        groundT = gameObject.transform.Find("Ground");
        defaultMaterial = groundT.GetComponent<Renderer>().sharedMaterial;
        gold = 0; water = 0; food = 0;

        base.initialize(gPos);
        gameObject.name = entityName + ": (" + (int)gridPosition.x + ", " + (int)gridPosition.y + ")";
    }

    public override void Update() {
        if(!UIPauseManager.paused && !updated){
            base.Update();
            if(isInMoveRange)
                inMoveRange();
        }
    }

    public void reset()
    {
        clickState = ClickSystem.ClickState.none;
        updated = false;
        isInMoveRange = false;
    }

    public override void noClickState()
    {
        groundT.GetComponent<Renderer>().sharedMaterial = defaultMaterial;
        base.noClickState();
    }

    public override void hoverClickState()
    {
        groundT.GetComponent<Renderer>().material.color = UIGameManager.assign? Color.yellow: Color.green;
        base.hoverClickState();
    }
    
    public override void clickClickState()
    {
        groundT.GetComponent<Renderer>().material.color = Color.red;
        base.clickClickState();
    }

    public void inMoveRange()
    {
        groundT.GetComponent<Renderer>().material.color = Color.blue;
        updated = true;
    }

    public override void clicked(System.Collections.Generic.List<GameObject> clickHistory)
    {
        base.clicked(clickHistory);
        ClickSystem.checkClickMoveUnit();
    }

    public List<Tile> getAdjacentTiles()
    {
        List<Tile> adjTiles = new List<Tile>();

        int width = TileMapGenerator.mapWidth;
        int height = TileMapGenerator.mapHeight;
        for(int y = -1; y < 2; y++)
            for(int x = -1; x < 2; x++)
                if(gridPosition.x+x >= 0 && gridPosition.y+y >= 0 & gridPosition.x + x < width && gridPosition.x + x > -1 && gridPosition.y + y < height && gridPosition.y + y > -1)
                    adjTiles.Add(TileMapGenerator.tiles[(int)gridPosition.y+ y, (int)gridPosition.x + x]);

        return adjTiles;
    }
}