using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeScaler : MonoBehaviour {

	public void SetTimeScale(float scale)
    {
        Time.timeScale = scale;
    }
}
