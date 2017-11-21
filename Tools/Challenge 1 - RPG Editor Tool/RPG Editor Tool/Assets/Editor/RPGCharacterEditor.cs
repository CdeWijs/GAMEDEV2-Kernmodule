using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System;

[CustomEditor(typeof(RPGCharacter))]
[CreateAssetMenu(fileName = "New Character", menuName = "RPG Character")]
public class RPGCharacterEditor : Editor {

    private RPGCharacter character;
    private static bool showBackpack = false;


    public void OnEnable() {
        character = (RPGCharacter)target;
    }

    public override void OnInspectorGUI() {

        // CHARACTER NAME
        character.characterName = EditorGUILayout.TextField("Name", character.characterName);

        // HAIR/EYE COLOR
        character.hairColor = EditorGUILayout.ColorField("Hair color", character.hairColor);
        character.eyeColor = EditorGUILayout.ColorField("Eye color", character.eyeColor);

        // LENGTH
        character.length = EditorGUILayout.Slider("Length", character.length, 140f, 200f);

        // BACKPACK
        showBackpack = EditorGUILayout.Foldout(showBackpack, "Backpack");
        
        if (showBackpack) {
            GUILayout.BeginHorizontal();
            GUILayout.Label("Backpack Items");
            // Create a list of backpack items.
        }

        EditorUtility.SetDirty(character);
    }
}
