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
    public override IEnumerator TimeAction(Action callback = null)
    {
        if (!MapManager.instance.isCreated )
        {
            Debug.LogWarning("맵이 안만들어 져 있음.");
            yield break;
        }
        var cubeList = cubeParent.GetComponentsInChildren<Cube>();
        Time.timeScale = 0.5f;
        foreach (var cube in cubeList)
        {
            cube.gameObject.AddComponent<Rigidbody>();
            cube.transform.localPosition = Vector3.zero;
        }
        yield return new WaitForSeconds(destroyTime);

        foreach (var cube in cubeList)
        {
            cube.GetComponent<KeepRotation>().enabled = true;
            cube.Release();
            Destroy(cube.GetComponent<Rigidbody>());
        }
        Time.timeScale = 1f;
        StartCoroutine( debris.AddCubeToDebri(cubeList, 2f));
               
        MapManager.instance.Clear();
        if (callback != null)
            callback();
        yield return null;
    }

    


}
