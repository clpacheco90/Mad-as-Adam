using UnityEngine;
using System.Collections;

public class CameraChangeScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetAxis ("Horizontal") > 0) {
			GameObject.Find ("cam2").camera.cullingMask |= 1 << (LayerMask.NameToLayer("Enemy"));
			GameObject.Find ("cam2").camera.cullingMask &=~(1 << (LayerMask.NameToLayer("Real")));
			GameObject.Find ("cam1").camera.cullingMask |= 1 << (LayerMask.NameToLayer("Real"));
			GameObject.Find ("cam1").camera.cullingMask &=~(1 << (LayerMask.NameToLayer("Enemy")));
		

				}
		if(Input.GetAxis("Horizontal") < 0) {
		

			GameObject.Find ("cam2").camera.cullingMask |= 1 << (LayerMask.NameToLayer("Real"));
			GameObject.Find ("cam2").camera.cullingMask &=~(1 << (LayerMask.NameToLayer("Enemy")));	
			GameObject.Find ("cam1").camera.cullingMask |= 1 << (LayerMask.NameToLayer("Enemy"));
			GameObject.Find ("cam1").camera.cullingMask &=~(1 << (LayerMask.NameToLayer("Real")));		


				}
	
	}
}
