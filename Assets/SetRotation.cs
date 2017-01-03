using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetRotation : MonoBehaviour {

    public Vector3 Euler;

	public void SetRotationInInspector()
    {
        StartCoroutine(TweenTrans.Rotation(transform, Quaternion.Euler(Euler), 0.2f));
    }

    

}
