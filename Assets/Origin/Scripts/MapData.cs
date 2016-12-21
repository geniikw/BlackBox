using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MapData : ScriptableObject
{
    public Vector3 mapSize;
    public List<Vector3> route;
    public int cutLimit = 0;
}
