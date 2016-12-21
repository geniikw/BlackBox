using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingame : MonoBehaviour {

    public CardBox cardBox;
    
    public void Play()
    {
        StartCoroutine(PlayRoutine());
    }

    IEnumerator PlayRoutine()
    {
        yield return null;
    }
    
    Vector3 GetCardDir(card c)
    {
        switch (c)
        {
            case card.Back:return new Vector3(1, 0, 0);              
            case card.Down:return new Vector3(0, -1, 0);
            case card.Front:return new Vector3(-1, 0, 0);
            case card.Left:return new Vector3(0, 0, -1);
            case card.Right:return new Vector3(0, 0, 1);
            case card.Up:return new Vector3(0, 1, 0);
            case card.None:return new Vector3(0, 0, 0);
        }
        return Vector3.zero;
    }


}
