using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepRotation : MonoBehaviour {

    public float speed = 1f;
    public Vector3 rotate;

	// Update is called once per frame
	void Update () {
        transform.Rotate(rotate  *speed, Space.Self);
	}
}
