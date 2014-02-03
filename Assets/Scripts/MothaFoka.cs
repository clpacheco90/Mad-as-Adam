using UnityEngine;
using System.Collections;

public class MothaFoka : MonoBehaviour {

    public string snce;
    private bool gl;
	// Update is called once per frame
	void Update () {
        
        
        if (!gl) {
            if (Input.anyKey) {
                GameObject.Find("AutoFadeLoad").GetComponent<LoadLevel>()._autoFadeConfig.nextScene = snce;
                GameObject.Find("AutoFadeLoad").GetComponent<LoadLevel>().enabled = true;
                gl = true;
            }
        }
	}
}
