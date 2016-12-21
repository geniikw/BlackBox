using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class Debris : MonoBehaviour {

    public int debrisCubeCount = 20;
    public float debrisCubeMaxDistance = 40f;
    public float debrisCubeMinDistance = 15f;
    public GameObject cubePrefab;
    public float debrisCubeSize = 0.2f;
    public float moveTime = 0.2f;

    private WaitForSeconds m_moveTime;

	// Use this for initialization
	void Start () {
	    for(int n = 0; n< debrisCubeCount; n++)
        {
            var go = Instantiate(cubePrefab);
            InitializeCube(go);
            go.transform.position = GetRandomDebriPosition();
        }
        m_moveTime = new WaitForSeconds(moveTime);
	}

    public Vector3 GetRandomDebriPosition()
    {
        return transform.TransformPoint( Random.onUnitSphere * Random.Range(debrisCubeMinDistance,debrisCubeMaxDistance ));
    }
    
    public void InitializeCube(GameObject cube)
    {
        cube.transform.SetParent(transform);
        cube.GetComponent<KeepRotation>().rotate = Random.onUnitSphere;
        cube.transform.localScale = Vector3.one * debrisCubeSize;
        cube.GetComponent<MeshRenderer>().material = GameResources.GetRandomMaterial();
        
    }

    public IEnumerator AddCubeToDebri(IEnumerable<Cube> list,float time)
    {
        foreach(var cube in list)
        {
            InitializeCube(cube.gameObject);
            StartCoroutine(TweenTransform.Position(cube.transform, GetRandomDebriPosition(), moveTime));
        }
        yield return m_moveTime;
    }

    public List<Cube> GetCubeFromDebri(int count)
    {
        if(count > transform.childCount)
        {
            var toAdd = count -transform.childCount;
            for(int n =0;n< toAdd; n++)
            {
                var go = Instantiate(cubePrefab);
                InitializeCube(go);
                go.transform.localPosition = GetRandomDebriPosition();
            }
        }

        return GetComponentsInChildren<Cube>().ToList().GetRange(0, count);
    }

    
}
