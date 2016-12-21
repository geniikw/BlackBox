using UnityEngine;
using UnityEditor;
using NUnit.Framework;

public class MapTest {

	[Test]
	public void MapDataCheck() {
		
        for(int n =0; n< GameResources.GetMapDataCount(); n++)
        {
            var data = GameResources.GetMapData(n);
            if(data.route.Count <= 2)
                Assert.Fail("Stage" + (n+1) + " 길이 너무 짧습니다. data.route.Count : " + data.route.Count);


            Vector3 prv = data.route[0] -Vector3.down;
            foreach (var routeCoord in data.route)
            {
                if(routeCoord.x <= 0 || routeCoord.x >= data.mapSize.x-1)
                {
                    Assert.Fail("Stage" + (n + 1) + " x값이 범위를 벗어납니다. x : " + routeCoord.x + " maxX : " + (data.mapSize.x - 1));
                }
                if (routeCoord.y < 0 || routeCoord.y > data.mapSize.y-1 )
                { 
                    Assert.Fail("Stage" + (n + 1) + " y값이 범위를 벗어납니다. y : " + routeCoord.y + " maxY : " + (data.mapSize.y - 1));
                }
                if (routeCoord.z <= 0 || routeCoord.z >= data.mapSize.z - 1)
                {
                    Assert.Fail("Stage" + (n + 1) + " z값이 범위를 벗어납니다. z : " + routeCoord.z + " maxZ : " + (data.mapSize.z - 1));
                }
                if(Vector3.Distance(routeCoord,prv) != 1)
                {
                    Assert.Fail("Stage" + (n + 1) + " 길이 이어지지 않습니다.");
                }
                prv = routeCoord;
            }
            if(prv.y != data.mapSize.y - 1)
            {
                Assert.Fail("Stage" + (n + 1) + " 최종 길이 최상단이 아닙니다. y : " + prv.y + " maxY : "+(data.mapSize.y-1));
            }
        }
	}
}
