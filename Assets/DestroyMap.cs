using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoodNightMypi.TimeAction;
using System;

public class DestroyMap : TimeBehaviour
{
    public Debris debris;
    public Transform cubeParent;
    public float destroyTime = 1f;
    public int destroyCubeLimit =50;
    public override IEnumerator TimeAction(Action callback = null)
    {
        if (!MapManager.instance.isCreated )
        {
            Debug.LogWarning("맵이 안만들어 져 있음.");
            yield break;
        }
        var cubeList = cubeParent.GetComponentsInChildren<Cube>();
        var cubeCount = 0;
        List<Cube> other = new List<Cube>();
        List<Cube> destroy = new List<Cube>();
        foreach (var cube in cubeList)
        {
            if(cubeCount > destroyCubeLimit)
            {
                other.Add(cube);
            }
            else
            {
                cube.gameObject.AddComponent<Rigidbody>();
                cube.transform.localPosition = Vector3.zero;
                destroy.Add(cube);
                cubeCount++;
            }
        }
        StartCoroutine(debris.AddCubeToDebri(other, destroyTime));
        yield return new WaitForSeconds(destroyTime);

        foreach (var cube in cubeList)
        {
            cube.GetComponent<KeepRotation>().enabled = true;
            Destroy(cube.GetComponent<Rigidbody>());
        }

        StartCoroutine( debris.AddCubeToDebri(destroy, 2f));
               
        MapManager.instance.Clear();
        if (callback != null)
            callback();
        yield return null;
    }
}
