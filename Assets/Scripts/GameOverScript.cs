using UnityEngine;
using System.Collections;

public class GameOverScript : MonoBehaviour {

	public int gameScene;
	public int menuScene;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKey (KeyCode.Space) ) {
			Application.LoadLevel(gameScene);
		}
		if (Input.GetKey (KeyCode.Q)) {
			Application.LoadLevel(menuScene);
				}

	
	}
}
