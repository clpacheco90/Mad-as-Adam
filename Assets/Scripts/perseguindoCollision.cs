using UnityEngine;
using System.Collections;

public class perseguindoCollision : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnTriggerEnter2D(Collider2D other){
		//dar um nome melhor ao objeto que possui esse collider
		if(other.gameObject.name.Equals("sprite")){
			transform.parent.gameObject.GetComponent<enfermeiroScript>().triggerFollow = true;
		}
	}
	
	public void OnTriggerExit2D(Collider2D other){
		//dar um nome melhor ao objeto que possui esse collider
		if(other.gameObject.name.Equals("sprite")){
			transform.parent.gameObject.GetComponent<enfermeiroScript>().triggerFollow = false;
		}
	}
}

