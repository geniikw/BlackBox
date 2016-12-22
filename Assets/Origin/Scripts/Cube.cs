using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Linq;
/// <summary>
/// 종속 : Mapmanager.
/// </summary>
public class Cube : MonoBehaviour, IDragHandler ,IPointerUpHandler, IPointerDownHandler{

    public Vector3 coord;

    static bool isInput = false;
    static Cube current;
    static List<Cube> selectList;

    static bool isPlus;
    static bool isX;
    static bool isInverse;
    static float submitDistanceRatio = 0.007f;
    float submitDistance { get { return 400f * submitDistanceRatio; } }
    float dragDistance = 0f;

    public void Init(Vector3 coord)
    {
        this.coord = coord;
    }
    
    public Vector3 GetInitPosition
    {
        get
        {
            return transform.TransformPoint(coord - (MapManager.instance.currentSize - Vector3.one) / 2f);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if(isInput && current == this)
        {
            var move = eventData.position - eventData.pressPosition;
            Vector3 results;
            
            move.y = 0;
            var ww = Camera.main.ScreenToWorldPoint(Vector3.zero) - Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0));
            //뭔가 병신같이 복잡하다. 똑똑한사람은 심플하게 솔루션이 있겠지... 흑흑
            
            move.x /= Screen.width / Mathf.Abs(ww.magnitude);
            if (isPlus && !isInverse && move.x > 0)
                move.x = 0;
            else if (isPlus && isInverse && move.x < 0)
                move.x = 0;
            else if (!isPlus && !isInverse && move.x < 0)
                move.x = 0;
            else if (!isPlus && isInverse && move.x > 0)
                move.x = 0;

            if (isX)
                results = isInverse? move:-move;
            else
                results = new Vector3(0, 0, isInverse? move.x:-move.x );

            dragDistance = Mathf.Abs(move.x);
            GameResources.GetMaterial(7).color = Color.Lerp(Color.white, Color.red, dragDistance / (submitDistance));
            
            MapManager.instance.handler.localPosition = results;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (isInput && current == this)
        {
            isInput = false;
            current = null;

            selectList.ForEach(c => {
                c.GetComponent<MeshRenderer>().material = GameResources.GetRandomMaterial();
            });

            GameResources.GetMaterial(7).color = Color.white;
            
            if (dragDistance > submitDistance)
            {
                Debug.Log("Divide Submit");
                TinySceneManager.instance.SetScene("CutView");
            }
            else
            {
                MapManager.instance.transform.Find("Handler").localPosition = Vector3.zero;
                selectList.ForEach(c => {
                    c.transform.SetParent(MapManager.instance.transform.Find("Cubes"));
                });
            }

            selectList.Clear();
            dragDistance = 0f;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (isInput)
            return;

        if (MapManager.instance.GetPosition(coord) != transform.position)
        {
            return;
        }

        isInput = true;
        current = this;
        var middle =( MapManager.instance.currentSize - Vector3.one)/2f;
        var list = MapManager.instance.listCube;

        ///hard coding... haha 
        if (Camera.main.transform.position.x == -60)
        {
            isX = true;
            if (Camera.main.transform.position.z > 0)
                isInverse = false;
            else
                isInverse = true;

        }
        else
        {
            isX = false;
            if (Camera.main.transform.position.x > -50)
                isInverse = true;
            else
                isInverse = false;
        }

        if (isX)
        {

            if (middle.x > coord.x)
                isPlus = false;
            else
                isPlus = true;

        }
        else
        {

            if (middle.z > coord.z)
                isPlus = false;
            else
                isPlus = true;
        }

        if(isX)
            selectList = list.Where(c => isPlus ? (c.coord.x >= coord.x) : (c.coord.x <= coord.x)).ToList();
        else
            selectList = list.Where(c => isPlus ? (c.coord.z >= coord.z) : (c.coord.z <= coord.z)).ToList();

        MapManager.instance.handler.localPosition = Vector3.zero;
        selectList.ForEach(c => {
            c.transform.SetParent(MapManager.instance.handler.transform);
            c.GetComponent<MeshRenderer>().material = GameResources.GetMaterial(7);
        });


    }
}
