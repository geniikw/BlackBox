using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitPosition : MonoBehaviour {

    Vector3 initPosition;
    Quaternion initQ;
	// Use this for initialization
	void Start () {
        initPosition = transform.position;
        initQ = transform.rotation;
    }

    public void Init()
    {
        transform.position = initPosition;
        transform.rotation = initQ;
    }
}
