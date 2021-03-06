﻿using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Grid_SCR), true)] 
public class Grid_ES : Editor
{

    public bool showDefault = false;

    override public void OnInspectorGUI()
    {
        showDefault = EditorGUILayout.Toggle("Show default options", showDefault);
        EditorGUILayout.Space();
        EditorGUILayout.Space();

        Grid_SCR gridScript = target as Grid_SCR;

        if (showDefault)
        {
            DrawDefaultInspector();
        }
        else
        {
            gridScript.gridResolution = EditorGUILayout.IntField("Resolution of grid", gridScript.gridResolution);
        }

        // Save all changes
        EditorUtility.SetDirty(target);
        EditorUtility.SetDirty(this);
        EditorUtility.SetDirty(gridScript);
    }
}