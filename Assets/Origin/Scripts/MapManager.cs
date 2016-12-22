using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class MapManager : MonoBehaviour {
    static MapManager m_instance;
    public static MapManager instance { get { return m_instance; } }

    public GameObject cubePrefab;
    public List<Cube> listCube = new List<Cube>();
    public Debris debris;
    public Transform handler;

    public float dragSpeed = 10f;

    int m_stage = 0;
    public int stage
    {
        set
        {
            var data = GameResources.GetMapData(value);
            currentSize = data.mapSize;
            currentRoute = data.route;
            m_stage = value;
        }
        get
        {
            return m_stage;
        }
    }
    public int currentStageCubeCount
    {
        get
        {
            return (int)(currentSize.x * currentSize.y * currentSize.z) - currentRoute.Count;
        }
    }

    public Vector3 currentSize;
    public List<Vector3> currentRoute;

    public bool isCreated { get
        {
            return listCube.Count != 0;
        }
    }

    private void Awake()
    {
        m_instance = this;
        listCube.Clear();
    }

    public void Clear()
    {
        listCube.Clear();
        debris.AddCubeToDebri(transform.Find("Cubes").GetComponentsInChildren<Cube>(),0.2f);
    }

    public void CreateMap(int stage)
    {
        var data = GameResources.GetMapData(stage);
        CreateMap(data.mapSize, data.route);
    }
    public void CreateMap(Vector3 mapSize, List<Vector3> route)
    {
        if (isCreated)
        {
            foreach(var cube in listCube)
            {
                Destroy(cube.gameObject);
            }
            listCube.Clear();
        }

        currentSize = mapSize;

        var minus = (mapSize - Vector3.one)/2f;

        for (int x = 0; x < mapSize.x; x++)
        {
            for (int y = 0; y < mapSize.y; y++)
            {
                for (int z = 0; z < mapSize.z; z++)
                {
                    if (route.Contains(new Vector3(x, y, z)))
                        continue;
                    var cube = Instantiate(cubePrefab);
                    cube.name = "x:" + x + "y:" + y + "z:" + z;
                    cube.transform.SetParent(transform.Find("Cubes"));
                    cube.transform.localPosition = new Vector3(x, y, z) - minus;
                    cube.GetComponent<Cube>().Init(new Vector3(x, y, z));
                    listCube.Add(cube.GetComponent<Cube>());
                    cube.GetComponent<MeshRenderer>().material = GameResources.GetMaterial(Random.Range(0, GameResources.MaxColor));
                }
            }
        }
        currentRoute = route;
    }

    public Vector3 GetPosition(Vector3 coord)
    {
        return transform.TransformPoint(coord - (currentSize-Vector3.one)/2);
    }
    public Vector3 GetPosition(float x, float y, float z) {
        return GetPosition(new Vector3(x, y, z));
    }

    public Vector3 GetStartPosition()
    {
        return GetPosition(currentRoute[0]) + new Vector3(0f,-1f,0f);
    }
    public Vector3 GetRoutePosition(int idx)
    {
        return GetPosition(currentRoute[idx]);
    }
    public Vector3 GetCamPosition(int idx)
    {
        var pos = GetRoutePosition(idx);
        pos += Vector3.back * (pos.z+2f);
        return pos;
    }

    public void Receive(GameObject go)
    {
        Debug.Log("receive");
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(MapManager))]
public class MapManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        
        var data = GameResources.GetAllMapData();
        int n = 0;

        if (Application.isPlaying)
            return;

        foreach(var d in data)
        {
            if (GUILayout.Button(n++ +"Stage"))
            {
                (target as MapManager).stage = n-1;
            }
        }
    }
}

#endif