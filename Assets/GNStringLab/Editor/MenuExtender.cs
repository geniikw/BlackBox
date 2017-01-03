using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

namespace GoodNightMypi.StringLab
{
    public static class ScriptableObjectUtility
    {
        /// <summary>
        //	This makes it easy to create, name and place unique new ScriptableObject asset files.
        /// </summary>
        public static void CreateAsset<T>() where T : ScriptableObject
        {
            T asset = ScriptableObject.CreateInstance<T>();

            string path = AssetDatabase.GetAssetPath(Selection.activeObject);
            if (path == "")
            {
                path = "Assets";
            }
            else if (Path.GetExtension(path) != "")
            {
                path = path.Replace(Path.GetFileName(AssetDatabase.GetAssetPath(Selection.activeObject)), "");
            }

            string assetPathAndName = AssetDatabase.GenerateUniqueAssetPath(path + "/New " + typeof(T).ToString() + ".asset");

            AssetDatabase.CreateAsset(asset, assetPathAndName);

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            EditorUtility.FocusProjectWindow();
            Selection.activeObject = asset;
        }
    }

    public class MenuExtender
    {
        [MenuItem("Asset/Create/StringTable")]
        public static void CreateStringTable()
        {
            ScriptableObjectUtility.CreateAsset<LanguageSet>();
        }

        public static void Serialilze(StringManager st)
        {
            //using (StreamWriter sw = new StreamWriter(FilePath))
            //{
            //    var wl = "Key";
            //    st.languageTable.ForEach(s => wl += (", " + s));
            //    sw.WriteLine(wl);

            //    int n = 0;
            //    foreach(var pair in st.stringTable)
            //    {
            //        wl = pair.Key;
            //        foreach(var v in pair.Value)
            //        {
            //            wl += ", " + v;
            //        }
            //        n++;
            //        sw.WriteLine(wl);
            //    }
            //}
        } 
        public static void Deserialize()
        {

            //if (!File.Exists(FilePath))
            //    return;

            //using (StreamReader sr = new StreamReader(FilePath))
            //{
            //    var line = sr.ReadLine();
                
            //    while (!sr.EndOfStream)
            //    {




            //    }
            //}
        }
    }


}

