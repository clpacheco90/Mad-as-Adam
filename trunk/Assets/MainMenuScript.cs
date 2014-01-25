using UnityEngine;
using System.Collections;

public class MainMenuScript : MonoBehaviour {

	public int firstStage;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.anyKeyDown) {
			Application.LoadLevel(firstStage);
				}
	
	}
}
