using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

public class PlatformerLevelWindow : EditorWindow {
    
    public ColorMappingList colorMappings;
    public LevelObjectList levelObjects;
    public Texture2D map;
    public GameObject parentObject;
    
    private Vector2 scrollPos;

    [MenuItem("My Tools/Platformer Level Editor")]
    static void Init() {
        // Get existing open window, or if none make a new one
        PlatformerLevelWindow window = (PlatformerLevelWindow)GetWindow(typeof(PlatformerLevelWindow));
        window.Show();
    }

    private void OnEnable() {
        if (EditorPrefs.HasKey("ObjectPath")) {
            string objectPath = EditorPrefs.GetString("ObjectPath");
            colorMappings = (ColorMappingList)AssetDatabase.LoadAssetAtPath(objectPath, typeof(ColorMappingList));
        }
    }

    private void OnGUI() {
        EditorGUILayout.Space();

        // If color mapping list doesn't exist yet
        GUILayout.BeginHorizontal();
        GUILayout.Space(10);
        if (GUILayout.Button("Open List")) {
            OpenList();
        }
        if (colorMappings) {
            if (GUILayout.Button("Select Existing List")) {
                Selection.activeObject = colorMappings;
            }
        }
        
        if (GUILayout.Button("New List")) {
            CreateNewList();
        }
        GUILayout.EndHorizontal();

        // If color mapping list exists
        if (colorMappings != null) {
            EditorGUILayout.Space();

            GUILayout.Label("Color Mapping", EditorStyles.boldLabel);

            EditorGUILayout.Space();

            colorMappings.texture = (Texture2D)EditorGUILayout.ObjectField("Texture", colorMappings.texture, typeof(Texture2D), true);
            map = colorMappings.texture;

            EditorGUILayout.Space();

            scrollPos = EditorGUILayout.BeginScrollView(scrollPos);
            // Show as many colors/prefabs as there are in colorMappings
            if (colorMappings.mappings.Count > 0) {
                foreach (ColorMapping mapping in colorMappings.mappings) {
                    EditorGUILayout.BeginHorizontal();
                    GUILayout.Label("Color");
                    mapping.color = EditorGUILayout.ColorField( mapping.color);
                    GUILayout.Label("Prefab");
                    mapping.prefab = (GameObject)EditorGUILayout.ObjectField(mapping.prefab, typeof(GameObject), true);
                    EditorGUILayout.EndHorizontal();
                }
            }
            GUILayout.EndScrollView();

            GUILayout.Space(20);

            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("New Mapping")) {
                ColorMapping newMapping = new ColorMapping();
                newMapping.color = Color.white;
                newMapping.prefab = null;
                colorMappings.mappings.Add(newMapping);
            }

            if (GUILayout.Button("Delete Last Mapping")) {
                colorMappings.mappings.RemoveAt(colorMappings.mappings.Count - 1);
            }
            EditorGUILayout.EndHorizontal();

            // Generate level from current texture
            if (GUILayout.Button("Generate Level")) {
                parentObject = new GameObject("Generated Tiles");
                GenerateLevel();
            }
        }

        EditorGUILayout.Space();
        
        if (GUI.changed) {
            EditorUtility.SetDirty(colorMappings);
        }
    }

    void CreateNewList() {
        colorMappings = CreateColorMappingList.Create();
        if (colorMappings) {
            colorMappings.mappings = new List<ColorMapping>();
            // Get current string from scriptable object (colorMappings) and assign to key.
            string relPath = AssetDatabase.GetAssetPath(colorMappings);
            EditorPrefs.SetString("ObjectPath", relPath);
            Selection.activeObject = colorMappings;
        }
    }

    void OpenList() {
        string absPath = EditorUtility.OpenFilePanel("Select Color Mapping List", "", "");
        if (absPath.StartsWith(Application.dataPath)) {
            string relPath = absPath.Substring(Application.dataPath.Length - "Assets".Length);
            colorMappings = (ColorMappingList)AssetDatabase.LoadAssetAtPath(relPath, typeof(ColorMappingList));

            if (colorMappings) {
                EditorPrefs.SetString("ObjectPath", relPath);
            }
            if (colorMappings.mappings == null) {
                colorMappings.mappings = new List<ColorMapping>();
            }

            Selection.activeObject = colorMappings;
        }
    }

    [ExecuteInEditMode]
    public void GenerateLevel() {
        // Iterate over pixels in map.
        for (int x = 0; x < map.width; x++) {
            for (int y = 0; y < map.height; y++) {
                GenerateTile(x, y);
            }
        }
    }

    public void GenerateTile(int x, int y) {
        // Read color from pixel in map
        Color pixelColor = map.GetPixel(x, y);

        if (pixelColor.a == 0) {
            // The pixel is transparent.
            return;
        }

        foreach (ColorMapping mapping in colorMappings.mappings) {
            if (mapping.color.Equals(pixelColor)) {
                Vector2 position = new Vector2(x, y);

                Instantiate(mapping.prefab, position, Quaternion.identity, parentObject.transform);
            }
        }
    }
}
