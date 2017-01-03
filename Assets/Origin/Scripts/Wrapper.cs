using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wrapper : MonoBehaviour {
    
	public void SetTimeScale(float scale)
    {
        Time.timeScale = scale;
    }
    public void AppExit()
    {
        Application.Quit();
    }
}
