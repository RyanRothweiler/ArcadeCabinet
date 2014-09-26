using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(BossBody_SCR), true)]
public class BossBody_ES : Editor
{

    public bool showDefault = false;


    override public void OnInspectorGUI()
    {
        serializedObject.Update();
        BossBody_SCR bossScript = target as BossBody_SCR;


        showDefault = EditorGUILayout.Toggle("Show default options", showDefault);
        EditorGUILayout.Space();
        EditorGUILayout.Space();

        if (showDefault)
        {
            DrawDefaultInspector();
        }
        else
        {
            if (bossScript.healthBar == null)
            {
                bossScript.healthBar = EditorGUILayout.ObjectField("Health bar", bossScript.healthBar, typeof(GameObject), false) as GameObject;
            }

            bossScript.currHealth = EditorGUILayout.FloatField("Current Health", bossScript.currHealth);
            bossScript.maxHealth = EditorGUILayout.FloatField("Max Health", bossScript.maxHealth);
        }

        // Save all changes
        EditorUtility.SetDirty(target);
        EditorUtility.SetDirty(this);
        EditorUtility.SetDirty(bossScript);
    }
}