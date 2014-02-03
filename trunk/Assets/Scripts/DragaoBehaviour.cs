using UnityEngine;
using System.Collections;

public class DragaoBehaviour : MonoBehaviour {

	
	Vector3 _posInicial;
	public float offsetX;
	float velX = 7;
	float direcao = -1;
	// Use this for initialization
	void Start () {
		_posInicial = transform.position;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(Mathf.Abs(transform.position.x)<=offsetX){
			Destroy(gameObject);
		}else{
			rigidbody2D.velocity = new Vector2(velX*direcao,rigidbody2D.velocity.y);
		}
	}
	void OnTriggerEnter2D(Collider2D other)
	{

		if (!other.CompareTag("Player")) return;

		if (!other.GetComponent < PlayerController >()._character2D.Movement.isDead)
		{
		
			other.GetComponent < PlayerController >()._character2D.Movement.isDead =true;
			other.GetComponent < PlayerController >()._character2D.Movement.enabled = false;
			other.GetComponent < PlayerController >()._character2D.Jump.enabled = false;
			other.GetComponent < PlayerController >()._character2D.Movement.speed=0;

		}



		
		
	}

}