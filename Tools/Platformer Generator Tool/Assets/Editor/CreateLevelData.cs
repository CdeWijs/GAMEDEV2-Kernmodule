using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CreateLevelData {

	[MenuItem("Assets/Create/Color Mapping List")]
    public static LevelData Create(string assetName) {
        // Create new scriptable object
        LevelData asset = ScriptableObject.CreateInstance<LevelData>();
        
        AssetDatabase.CreateAsset(asset, "Assets/" + assetName + " data.asset");
        AssetDatabase.SaveAssets();
        return asset;
    }
}
