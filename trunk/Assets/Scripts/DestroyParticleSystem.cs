using UnityEngine;
using System.Collections;


public class DestroyParticleSystem : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		// Destroy particle system when it "dies"
		if(!gameObject.particleSystem.IsAlive() )
		{
			GameObject.Destroy(gameObject);
		}
	}
}
