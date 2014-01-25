using UnityEngine;
using System.Collections;

public enum States{Procurando, Desconfiado, Perseguindo,Descansando};
public class enfermeiroScript : MonoBehaviour {

	public float areaRondaTam;
	public Vector2 posInicial;
	public float maxSpeed = 10.0f;
	public Vector2 direcao = new Vector2 (1,0); 
	private States state;
	public static bool triggerDesconfiada = false;
	public static bool triggerFollow = false;
	float _chasingCount = 0;
	private bool _isturning = false;

	// Use this for initialization
	void Start () {
		state = States.Procurando;
		direcao = new Vector2 (1,0); 
		posInicial = transform.position;
	}
	
	// Update is called once per frame
	void FixedUpdate () {


	
		Debug.Log(state);
		switch (state) 
		{
		case States.Procurando:
			//Debug.Log ("procurando" + rigidbody2D.velocity);

			//Debug.Log ("procurando" + rigidbody2D.velocity);
			
			if((direcao.x >= 0 && transform.position.x >= posInicial.x + areaRondaTam)
			   ||
			   (direcao.x <= 0 && transform.position.x <= (posInicial.x - areaRondaTam)))
			{
			
				direcao = -direcao;
				transform.Rotate(0,180,0);
			}
			if(triggerFollow && !GameObject.Find("Player").GetComponent<PlayerController>()._character2D.Movement.isSquatDown)
			{
				state = States.Perseguindo; 
				_chasingCount = 0;
			}
			else if(triggerDesconfiada && !GameObject.Find("Player").GetComponent<PlayerController>()._character2D.Movement.isSquatDown){
				state = States.Desconfiado;
				_chasingCount = 0;
			}
			if(_isturning != true)
			{
			   rigidbody2D.velocity = new Vector2(maxSpeed * direcao.x, rigidbody2D.velocity.y);
			}
		
			break;
			
		case States.Desconfiado:
			if(triggerDesconfiada){
				_chasingCount = 0;
			}
			if(_chasingCount>=1){
				_chasingCount = 0;
				direcao = -direcao;
				transform.Rotate(0,180,0);
				state = States.Procurando;
			}else{
				_chasingCount += Time.fixedDeltaTime;
				if(triggerFollow && !GameObject.Find("Player").GetComponent<PlayerController>()._character2D.Movement.isSquatDown){
					state = States.Perseguindo;
				}else{
					rigidbody2D.velocity = new Vector2(maxSpeed * direcao.x, rigidbody2D.velocity.y);
				}
			}
			break;
		case States.Perseguindo:
			if(triggerFollow){
				_chasingCount = 0;
			}
			if(_chasingCount>=1){
				_chasingCount = 0;
				direcao = -direcao;
				transform.Rotate(0,180,0);
				state = States.Descansando;
			}else{
				_chasingCount += Time.fixedDeltaTime;
				if(GameObject.Find("Player").transform.position.x - transform.position.x<=0){
					if(direcao.x == 1)	transform.Rotate(0,180,0);
					direcao = new Vector2 (-1,0);

				}else{
					if(direcao.x == -1)	transform.Rotate(0,180,0);

					direcao = new Vector2 (1,0);
				}
				rigidbody2D.velocity = new Vector2(maxSpeed * 1.5f * direcao.x, rigidbody2D.velocity.y);
			}
			break;
		case States.Descansando:
			if(_chasingCount>=2){
				_chasingCount = 0;
				state = States.Procurando;
			}else{
				_chasingCount += Time.fixedDeltaTime;
			}
			break;
		}
	}


}
