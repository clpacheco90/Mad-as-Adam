using UnityEngine;
using System.Collections;

public class CameraAspect2 : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void Awake ()
	{
		// Para cameras ficarem sempre na posicao certa, nao importa o aspecto
		float vertical_size = camera.orthographicSize * 2.0f;
		float horizontal_size = vertical_size * camera.aspect;

		transform.Translate (-horizontal_size/2,0,0);
		Debug.Log ("valor" + horizontal_size);
	}
}
