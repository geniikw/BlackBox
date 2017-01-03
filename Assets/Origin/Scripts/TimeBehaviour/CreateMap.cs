using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoodNightMypi.TimeAction;
using System;
using System.Linq;
public class CreateMap : TimeBehaviour {

    public Transform cubeParent;
    public Transform playerCube;

    public override IEnumerator TimeAction(Action callback = null)
    {
        var debri = MapManager.instance.debris;
        var mm = MapManager.instance;
        var cubes = debri.GetCubeFromDebri(MapManager.instance.currentStageCubeCount);

        int n = 0;
        StartCoroutine(TweenTrans.Tween(playerCube, mm.GetStartPosition(), Vector3.one * 0.9f, Vector3.zero, 0.2f));
   
        for (int x = 0; x < mm.currentSize.x; x++)
        {
            for (int y = 0; y < mm.currentSize.y; y++)
            {
                for (int z = 0; z < mm.currentSize.z; z++)
                {
                    if (mm.currentRoute.Contains(new Vector3(x, y, z)))
                        continue;
                    
                    var cube = cubes[n++];
                    cube.name = "x:" + x + "y:" + y + "z:" + z;
                    mm.listCube.Add(cube);
                    var pos = mm.GetPosition(new Vector3(x, y, z));
                    cube.transform.SetParent(cubeParent);
                    cube.GetComponent<KeepRotation>().enabled = false;
                    cube.Init(new Vector3(x, y, z));
                    StartCoroutine(TweenTrans.Tween(cube.transform, pos, Vector3.one, Vector3.zero, 0.4f));
                }
                //yield return new WaitForSeconds(0.1f);
            }
        }

       
        yield return null;
    }
    


}
