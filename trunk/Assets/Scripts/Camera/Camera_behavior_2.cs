using UnityEngine;
using System.Collections;

public class Camera_behavior_2 : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
		
		
		
	}
	
	// Update is called once per frame
	void Update () {

		//this.transform = GameObject.Find ("Player").transform;
		//ajustaCamera ();
		
		/*GameObject player = GameObject.Find("player");
		
		if (!movement.Instance.facing_right)
		{
		
			camera.cullingMask = -257;
			
		}
		else
		{
		
			camera.cullingMask = -513;
			
		}*/
		
	}

	void FixedUpdate(){
		ajustaCamera ();
	}
	
	void Awake ()
	{
		ajustaCamera ();

		
	}

	void ajustaCamera(){
		Camera cam2 = GameObject.Find ("cam2").camera;
		Vector3 novo = new Vector3 (GameObject.Find("Player").transform.position.x, -3, -10);
		transform.position = novo;
		float vertical_size = cam2.orthographicSize;
		float horizontal_size = vertical_size * cam2.aspect ;
		transform.Translate(-horizontal_size,0,0);
		
	}


}
