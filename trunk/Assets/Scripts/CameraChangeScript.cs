using UnityEngine;
using System.Collections;
using System.Collections.Generic; // pra poder usar Lista<>

public class CameraChangeScript : MonoBehaviour {

	// List of game objects
	private GameObject gos; 
	public GameObject poof; // Particle system
	private bool poofAtRightCam = true;

	// Use this for initialization
	void Start () {

		//Initializes the camera culling mask to the right, by default (before the player starts moving)
		GameObject.Find ("cam2").camera.cullingMask |= 1 << (LayerMask.NameToLayer("Enemy"));
		GameObject.Find ("cam2").camera.cullingMask &=~(1 << (LayerMask.NameToLayer("Real")));
		GameObject.Find ("cam1").camera.cullingMask |= 1 << (LayerMask.NameToLayer("Real"));
		GameObject.Find ("cam1").camera.cullingMask &=~(1 << (LayerMask.NameToLayer("Enemy")));
	

	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetAxis ("Horizontal") > 0) {

			//Changes the camera culling mask
			GameObject.Find ("cam2").camera.cullingMask |= 1 << (LayerMask.NameToLayer("Enemy"));
			GameObject.Find ("cam2").camera.cullingMask &=~(1 << (LayerMask.NameToLayer("Real")));
			GameObject.Find ("cam1").camera.cullingMask |= 1 << (LayerMask.NameToLayer("Real"));
			GameObject.Find ("cam1").camera.cullingMask &=~(1 << (LayerMask.NameToLayer("Enemy")));
		

			if(poofAtRightCam == true)
			{
				poofAtRightCam = false;

				//Extension method, to help find in the scene all game objects with a specific layer
				//gos = gameObject.FindGameObjectsWithLayer(8); // 8 = layer "Enemy"
				gos = GameObject.Find ("imaginario");

				// Instantiate the "poof" particle system in the position of all enemies, when they appear on the screen

				//Instantiate(poof, gos.transform.position, gos.transform.rotation);
//				foreach (GameObject go in gos)
//				{
//					Instantiate(poof, go.transform.position, go.transform.rotation  );
//				}
			}

		}
		if(Input.GetAxis("Horizontal") < 0) {
		
			//Changes the camera culling mask
			GameObject.Find ("cam2").camera.cullingMask |= 1 << (LayerMask.NameToLayer("Real"));
			GameObject.Find ("cam2").camera.cullingMask &=~(1 << (LayerMask.NameToLayer("Enemy")));	
			GameObject.Find ("cam1").camera.cullingMask |= 1 << (LayerMask.NameToLayer("Enemy"));
			GameObject.Find ("cam1").camera.cullingMask &=~(1 << (LayerMask.NameToLayer("Real")));		

			if(poofAtRightCam == false)
			{
				poofAtRightCam = true;

				//Extension method, to help find in the scene all game objects with a specific layer
//				gos = gameObject.FindGameObjectsWithLayer(8); // 8 = layer "Enemy"
//				//gos = GameObject.Find("enemy");
//				// Instantiate the "poof" particle system in the position of all enemies, when they appear on the screen
//				foreach (GameObject go in gos)
//				{
//					Instantiate(poof, go.transform.position, go.transform.rotation);
//				}							
			}

		}
	
	}
}
