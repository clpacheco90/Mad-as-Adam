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
		if(other.gameObject.tag.Equals("Enemy")){
			other.gameObject.GetComponent<EnemyRenderer>().setDomain(Domain.Imaginary);
		}
	}

	void OnTriggerExit2D(Collider2D other){
		if(other.gameObject.tag.Equals("Enemy")){
			other.gameObject.GetComponent<EnemyRenderer>().setDomain(Domain.Real);
		}
	}
}
