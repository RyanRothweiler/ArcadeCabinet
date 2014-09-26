using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(Gun_SCR), true)] 
public class Gun_ES : Editor
{
    public bool showDefault = false;

    void OnEnable()
    {

    }

    override public void OnInspectorGUI()
    {
        showDefault = EditorGUILayout.Toggle("Show default options", showDefault);
        EditorGUILayout.Space();
        EditorGUILayout.Space();

        if (showDefault)
        {
            DrawDefaultInspector();
        }
        else
        {
            serializedObject.Update();
            Gun_SCR gunScript = target as Gun_SCR;

            if (gunScript.bulletPreFab == null)
            {
                gunScript.bulletPreFab = EditorGUILayout.ObjectField("Bullet Object", gunScript.bulletPreFab, typeof(GameObject), false) as GameObject;
            }
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
            gunScript.reloadTime = EditorGUILayout.FloatField("Reload Time", gunScript.reloadTime);

            EditorGUILayout.Space();

            gunScript.damage = EditorGUILayout.FloatField("Damage", gunScript.damage);

            EditorUtility.SetDirty(target);
            EditorUtility.SetDirty(this);
            EditorUtility.SetDirty(gunScript);
        }
    }
}