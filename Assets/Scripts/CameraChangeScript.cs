using UnityEngine;
using System.Collections;
using System.Collections.Generic; // pra poder usar Lista<>

public class CameraChangeScript : MonoBehaviour {

	// List of game objects
	private List<GameObject> gos; 
	public GameObject poof; // Particle system
	private bool poofAtRightCam = true;
	//private bool dontPoof = true; // Don't poof at the start of the stage (when the player press a direction)

	// Use this for initialization
	void Start () {

		//Initializes the camera culling mask to the right, by default (before the player starts moving)
		GameObject.Find ("cam2").camera.cullingMask |= 1 << (LayerMask.NameToLayer("Imaginario"));
		GameObject.Find ("cam2").camera.cullingMask &=~(1 << (LayerMask.NameToLayer("Real")));
		GameObject.Find ("cam1").camera.cullingMask |= 1 << (LayerMask.NameToLayer("Real"));
		GameObject.Find ("cam1").camera.cullingMask &=~(1 << (LayerMask.NameToLayer("Imaginario")));
	

		// INITIAL POOF when the level starts
		if(poofAtRightCam == true /*&& dontPoof == false*/)
		{
			poofAtRightCam = false;
			
			//Extension method, to help find in the scene all game objects with a specific layer
			gos = gameObject.FindGameObjectsWithLayer(11); // 8 = layer "Enemy"
			//gos = GameObject.Find ("imaginario");
			
			// Instantiate the "poof" particle system in the position of all enemies, when they appear on the screen
			foreach (GameObject go in gos)
			{
				Instantiate(poof, go.transform.position, go.transform.rotation  );
			}
		}
		else if(poofAtRightCam == false /*&& dontPoof == false*/)
		{
			poofAtRightCam = true;
			
			//Extension method, to help find in the scene all game objects with a specific layer
			gos = gameObject.FindGameObjectsWithLayer(11); // 8 = layer "Enemy"
			//gos = GameObject.Find("enemy");
			
			// Instantiate the "poof" particle system in the position of all enemies, when they appear on the screen
			foreach (GameObject go in gos)
			{
				Instantiate(poof, go.transform.position, go.transform.rotation);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {

		//Debug.Log (dontPoof);

		if (Input.GetAxis ("Horizontal") > 0) 
		{
			//Changes the camera culling mask
			GameObject.Find ("cam2").camera.cullingMask |= 1 << (LayerMask.NameToLayer("Imaginario"));
			GameObject.Find ("cam2").camera.cullingMask &=~(1 << (LayerMask.NameToLayer("Real")));
			GameObject.Find ("cam1").camera.cullingMask |= 1 << (LayerMask.NameToLayer("Real"));
			GameObject.Find ("cam1").camera.cullingMask &=~(1 << (LayerMask.NameToLayer("Imaginario")));
		

			if(poofAtRightCam == true /*&& dontPoof == false*/)
			{
				poofAtRightCam = false;

				//Extension method, to help find in the scene all game objects with a specific layer
				gos = gameObject.FindGameObjectsWithLayer(11); // 8 = layer "Enemy"
				//gos = GameObject.Find ("imaginario");

				// Instantiate the "poof" particle system in the position of all enemies, when they appear on the screen
				foreach (GameObject go in gos)
				{
					Instantiate(poof, go.transform.position, go.transform.rotation  );
				}
			}

			// Just to not make the enemies poof when the level starts
			//if(dontPoof)
			//	dontPoof = false;
		}
		else if(Input.GetAxis("Horizontal") < 0) {
		
			//Changes the camera culling mask
			GameObject.Find ("cam2").camera.cullingMask |= 1 << (LayerMask.NameToLayer("Real"));
			GameObject.Find ("cam2").camera.cullingMask &=~(1 << (LayerMask.NameToLayer("Imaginario")));	
			GameObject.Find ("cam1").camera.cullingMask |= 1 << (LayerMask.NameToLayer("Imaginario"));
			GameObject.Find ("cam1").camera.cullingMask &=~(1 << (LayerMask.NameToLayer("Real")));		

			if(poofAtRightCam == false /*&& dontPoof == false*/)
			{
				poofAtRightCam = true;

				//Extension method, to help find in the scene all game objects with a specific layer
				gos = gameObject.FindGameObjectsWithLayer(11); // 8 = layer "Enemy"
				//gos = GameObject.Find("enemy");

				// Instantiate the "poof" particle system in the position of all enemies, when they appear on the screen
				foreach (GameObject go in gos)
				{
					Instantiate(poof, go.transform.position, go.transform.rotation);
				}
			}

			// Just to not make the enemies poof when the level starts
			//if(dontPoof)
			//	dontPoof = false;

		}
	
	}
}
