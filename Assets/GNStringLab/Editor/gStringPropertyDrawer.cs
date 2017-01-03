using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace GoodNightMypi.StringLab
{
    [CustomPropertyDrawer(typeof(gString))]
    public class gStringPropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            
 //           EditorGUI.PropertyField(position, targetString);
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUIUtility.singleLineHeight  *2f;
        }
    }

}


