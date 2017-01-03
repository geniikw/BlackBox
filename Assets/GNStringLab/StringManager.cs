using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
namespace GoodNightMypi.StringLab
{
    public class StringManager :MonoBehaviour
    {
        static StringManager instance;
        void Awake()
        {
            instance = this;
        }
        
        void OnDisable()
        {
            Debug.Log("destroy");
        }

        public LanguageSet st;

        Dictionary<string, string> m_sTable = new Dictionary<string, string>();
        
        public void Serialize()
        {
            //using (StreamWriter sw = new StreamWriter(FilePath))
            //{
            //    var wl = "Key";
            //    st.languageTable.ForEach(s => wl += ", " + s);
            //    sw.WriteLine(wl);

            //    foreach(var pair in st.stringTable)
            //    {
            //        wl = pair.Key;
            //        foreach(var s in pair.Value)
            //        {
            //            wl += ", " + s;
            //        }
            //        sw.WriteLine(wl);
            //    }
            //}
        }
        public void Deserialize()
        {
            //if (!File.Exists(FilePath))
            //    return;

            //st.languageTable.Clear();
            //st.stringTable.Clear();

            //using (StreamReader sr = new StreamReader(FilePath))
            //{
            //    var line = sr.ReadLine();

            //    var ls = line.Split(',').Skip(0); //"Key" 무시.
                
            //    foreach(var lan in ls)
            //    {
            //        st.languageTable.Add(lan.Trim());
            //    }

            //    while (!sr.EndOfStream)
            //    {
            //        line = sr.ReadLine();
            //        ls = line.Split(',');
            //        var key = ls.First().Trim();
            //        var data = new List<string>();
            //        foreach(var datus in ls.Skip(0))
            //        {
            //            data.Add(datus.Trim());
            //        }
            //        st.stringTable.Add(key, data);
            //    }
            //}
        }
    }

   
}
