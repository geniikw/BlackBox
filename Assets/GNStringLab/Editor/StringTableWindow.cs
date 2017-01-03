using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using GoodNightMypi.StringLab;
using System.Linq;

public class StringTableWindow : EditorWindow {

    List<SerializedObject> m_listSo = new List<SerializedObject>();
    List<string> m_languages = new List<string>();

    string m_toAddLang = "";

    [MenuItem("StringLab/Open")]
    public static void OpenWindow()
    {
        var w = CreateInstance<StringTableWindow>();
        w.titleContent = new GUIContent("StringTable");
        w.Show();
    }

    void OnEnable()
    {
        var ls = Resources.LoadAll<LanguageSet>("Languages/").ToList();
        m_languages.Clear();
        ls.ForEach(l => m_languages.Add(l.name));
        m_listSo.Clear();
        m_listSo.AddRange(ls.Select(s=>new SerializedObject(s)));
    }
    
    void OnGUI()
    {
        //draw key.
        m_listSo.ForEach(l => l.Update());
        var keys = m_listSo[0].FindProperty("keys");
        
        GUILayout.BeginHorizontal();

        GUILayout.BeginVertical(GUILayout.MaxWidth(100f));
        GUILayout.Label("Key");

        var size = m_listSo[0].FindProperty("keys").arraySize;
        for (int n =0; n< size; n++)
        {
            
            var sp = m_listSo[0].FindProperty("keys").GetArrayElementAtIndex(n);
            
            EditorGUILayout.PropertyField(sp, GUIContent.none);
          
        }

        GUILayout.EndVertical();

        for(int n = 0; n< m_languages.Count; n++)
        {

            GUILayout.BeginVertical(GUILayout.MaxWidth(500f));
            GUILayout.Label(m_languages[n]);
            if(m_listSo[n].FindProperty("values").arraySize != size)
            {
                Debug.LogWarning("언어셋 크기가 같아야함. size : "+size +", value.size : " + m_listSo[n].FindProperty("values").arraySize);
            }

            for(int i = 0; i< size; i++)
            {
                EditorGUILayout.PropertyField(m_listSo[n].FindProperty("values").GetArrayElementAtIndex(i),GUIContent.none);
            }
            GUILayout.EndVertical();
        }
  
        GUILayout.EndHorizontal();
        m_listSo.ForEach(l => l.ApplyModifiedProperties());

    }
}
