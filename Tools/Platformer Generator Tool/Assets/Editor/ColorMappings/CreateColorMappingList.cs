using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CreateColorMappingList {

	[MenuItem("Assets/Create/Color Mapping List")]
    public static ColorMappingList Create() {
        // Create new scriptable object
        ColorMappingList asset = ScriptableObject.CreateInstance<ColorMappingList>();

        AssetDatabase.CreateAsset(asset, "Assets/ColorMappingList.asset");
        AssetDatabase.SaveAssets();
        return asset;
    }
}
