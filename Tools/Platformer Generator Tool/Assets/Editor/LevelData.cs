using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelData : ScriptableObject {

    public Texture2D texture;
    public List<ColorMapping> mappings;
    public Object generatedTiles;
}