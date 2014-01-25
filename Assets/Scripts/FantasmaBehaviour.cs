using UnityEngine;
using System.Collections;

public class FantasmaBehaviour : MonoBehaviour {

	Vector3 _posInicial;
	public float offsetX;
	public float velX;
	public float direcao = 1;
	// Use this for initialization
	void Start () {
		_posInicial = transform.position;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(Mathf.Abs(transform.position.x-_posInicial.x)>=offsetX){
			Destroy(gameObject);
		}else{
			rigidbody2D.velocity = new Vector2(velX*direcao,rigidbody2D.velocity.y);
		}
	}
}
