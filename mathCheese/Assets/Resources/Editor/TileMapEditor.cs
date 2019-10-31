using System.Collections;
using UnityEngine;
using UnityEditor;

[CustomEditor (typeof (TileMapGenerator))]
public class TileMapEditor : Editor
{
    public override void OnInspectorGUI() {
		TileMapGenerator tileGen = (TileMapGenerator)target;

		if (DrawDefaultInspector ()) {
			if (tileGen.autoUpdate) {
                for(int i = tileGen.gameObject.transform.childCount-1; i > -1; i--){
                    DestroyImmediate(tileGen.gameObject.transform.GetChild(i).gameObject);
                }
				tileGen.buildMap();
			}
		}

		if (GUILayout.Button ("Generate")) {
            for(int i = tileGen.gameObject.transform.childCount-1; i > -1; i--){
                DestroyImmediate(tileGen.gameObject.transform.GetChild(i).gameObject);
            }
			tileGen.buildMap();
		}
	}
}
