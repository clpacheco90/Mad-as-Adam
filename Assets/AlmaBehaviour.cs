using UnityEngine;
using System.Collections;

public class AlmaBehaviour : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnTriggerEnter2D(Collider2D other){
		Debug.Log(other.gameObject.layer);
		if(other.gameObject.tag.Equals("Ground")){
			Debug.Log("depois");
			Destroy(gameObject);
		}
		
	}
}
