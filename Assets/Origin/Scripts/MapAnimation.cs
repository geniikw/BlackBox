using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoodNightMypi.TimeAction;

public class MapAnimation : TimeBehaviour {

    List<Transform> listChild = new List<Transform>();
    public Vector3 startCubePosition;
    public Vector2 startCubeArea;

    List<Vector3> listChildDefaultPosition = new List<Vector3>();

    public void Init()
    {
        for(int n = 0; n< transform.childCount; n++)
        {
            var child = transform.GetChild(n);
            listChild.Add(child);
            listChildDefaultPosition.Add(child.transform.position);
        }
    }

    public void Clear()
    {
        listChild.Clear();
    }

    public override IEnumerator TimeAction(System.Action callback)
    {
        listChild.ForEach(child => child.position = GetRandomPosition());
        listChild.ForEach(child => child.rotation = Random.rotation);
        WaitForSeconds wait = new WaitForSeconds(0.02f);
        for(int n =0;n<listChild.Count; n++)
        {
            StartCoroutine(TweenTransform.Position(listChild[n], listChildDefaultPosition[n], 0.5f));
            yield return wait;
        }
    }

    public IEnumerator Victory()
    {
        foreach(var child in listChild)
        {
            StartCoroutine(TweenTransform.Position(child, child.position+Random.onUnitSphere * 10f, 3f));
            Destroy(child.gameObject,3f);
        }
        yield return new WaitForSeconds(3f);
        listChild.Clear();
    }

    public IEnumerator Lose()
    {
        foreach (var child in listChild)
        {
            StartCoroutine(TweenTransform.Position(child,  child.transform.position +Vector3.down*10f, 3f));
            Destroy(child.gameObject, 3f);
        }
        yield return new WaitForSeconds(3f);
        listChild.Clear();
    }

    Vector3 GetRandomPosition()
    {
        var x = Random.Range(-startCubeArea.x / 2f, startCubeArea.x / 2f);
        var y = Random.Range(-startCubeArea.y / 2f, startCubeArea.y / 2f);
        return transform.position + startCubePosition + new Vector3(x, 0, y);
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawCube(transform.position + startCubePosition, new Vector3(startCubeArea.x,1f, startCubeArea.y));
    }

}




