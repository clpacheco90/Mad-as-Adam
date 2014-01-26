using UnityEngine;
using System.Collections;

public class PlayerVision : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnTriggerEnter2D(Collider2D other){
			if(other.gameObject.tag.Equals("Fantasma")){
				other.enabled = true;
			}
//		if(other.gameObject.tag.Equals("Arvore")){
//			other.gameObject.GetComponent<ArvoreBehaviour>().isReal = false;
//		}
	}

	void OnTriggerExit2D(Collider2D other){
			if(other.gameObject.tag.Equals("Fantasma")){
				other.enabled = false;
			}
//		if(other.gameObject.tag.Equals("Arvore")){
//			other.gameObject.GetComponent<ArvoreBehaviour>().isReal = true;
//		}
	} 
}
