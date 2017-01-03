using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GoodNightMypi.StringLab
{
    [System.Serializable]
    public struct gString 
    {
        public string key;
        static StringManager st;
        
        public gString(string key)
        {
            this.key = key;
        }
        //public static implicit operator string(gString i)
        //{
        //    if (st == null)
        //        return null;
        //    return st.GetString(i.key);
        //}
        public static implicit operator gString(string v)
        {
            return new gString(v);
        }
    }
}

