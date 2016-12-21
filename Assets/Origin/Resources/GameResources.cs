using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public enum card
{
    Up,
    Down,
    Right,
    Left,
    Front,
    Back,
    None
}

public static class GameResources {

    static readonly string DirectionPath = "Directions/";
    static readonly string MapDataPath = "StageData/";
    static readonly string ColorMaterialPath = "Materials/ColorCube/";
    public static int MaxColor = 7;
    public static Sprite GetDirection(card card)
    {
        return Resources.LoadAll<Sprite>(DirectionPath).First(sp=>sp.name==card.ToString());
    }
    public static MapData GetMapData(int stage)
    {
        return Resources.Load<MapData>(MapDataPath + "Stage_" + (stage+1));   
    }
    public static int GetMapDataCount()
    {
        return Resources.LoadAll<MapData>(MapDataPath).ToList().Count;
    }
    public static MapData[] GetAllMapData()
    {
        return Resources.LoadAll<MapData>(MapDataPath);
    }
    /// <summary>
    /// 0~6 = randomColor
    /// 7 = selectColor
    /// </summary>

    public static Material GetMaterial(int index)
    {
        return Resources.Load<Material>(ColorMaterialPath + "OutLineColor" + index);
    }
    public static Material GetRandomMaterial()
    {
        return GetMaterial(Random.Range(0,MaxColor));
    }


}
