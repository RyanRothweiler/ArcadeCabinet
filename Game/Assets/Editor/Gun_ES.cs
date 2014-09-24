using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(Gun_SCR))] 
public class Gun_ES : Editor
{

    void OnEnable()
    {

    }

    override public void OnInspectorGUI()
    {
        serializedObject.Update();
        Gun_SCR gunScript = target as Gun_SCR;

        gunScript.bulletPreFab = EditorGUILayout.ObjectField("Bullet Object", gunScript.bulletPreFab, typeof(GameObject), false) as GameObject;
        gunScript.spread = EditorGUILayout.FloatField("Spread", gunScript.spread);
        gunScript.knockBack = EditorGUILayout.FloatField("Knock Back", gunScript.knockBack);

        EditorGUILayout.Space();

        gunScript.automatic = EditorGUILayout.Toggle("Automatic", gunScript.automatic);
        if (gunScript.automatic)
        {
            gunScript.timeBetweenShots = EditorGUILayout.FloatField("Time Between Shots", gunScript.timeBetweenShots);
        }

        EditorGUILayout.Space();

        gunScript.clipSize = EditorGUILayout.IntField("Clip Size", gunScript.clipSize);
        gunScript.bulletsInClip = EditorGUILayout.IntField("Bullets In Clip", gunScript.bulletsInClip);
        gunScript.reloadTime = EditorGUILayout.FloatField("Reloat Time", gunScript.reloadTime);

        EditorUtility.SetDirty(target);
        EditorUtility.SetDirty(this);
        EditorUtility.SetDirty(gunScript);
    }
}