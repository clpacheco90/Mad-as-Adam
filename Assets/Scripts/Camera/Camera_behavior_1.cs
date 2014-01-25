using UnityEngine;
using System.Collections;

public class Camera_behavior_1 : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
		
		
		
	}
	
	// Update is called once per frame
	void Update () {
	
		//GameObject player = GameObject.Find("player");
		
		//if (movement.Instance.facing_right)
		//
		
		//	camera.cullingMask = -257;
			
		//}
		//else
		//{
		
		//	camera.cullingMask = -513;
			
		//}	
				
		
	}
	
	void Awake ()
	{
		
		float vertical_size = camera.orthographicSize * 2.0f;
		float horizontal_size = vertical_size * camera.aspect ;
			transform.Translate(horizontal_size/2,0,0);

		
	}
}
