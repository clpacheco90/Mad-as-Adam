using UnityEngine;
using System.Collections;

public enum States{Procurando, Desconfiado, Perseguindo,Descansando};
public class enfermeiroScript : MonoBehaviour {

	Animator anim_1;
	Animator anim_2;
	public float areaRondaTam;
	public Vector2 posInicial;
	public float maxSpeed = 10.0f;
	public Vector2 direcao = new Vector2 (1,0); 
	private States state;
	public bool triggerDesconfiada = false;
	public bool triggerFollow = false;
	float _chasingCount = 0;
	private bool _isturning = false;
	public bool red_type = false;

	// Use this for initialization
	void Start () {
		state = States.Procurando;



		anim_1 = GetComponentsInChildren<Animator>()[0];
		anim_2 = GetComponentsInChildren<Animator>()[1];


		direcao = new Vector2 (1,0); 
		posInicial = transform.position;

	

		//anim_1 = Get ComponentInChildren<Animator>();
		//anim_2 = GetComponentInChildren<Animator>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {


		anim_1.SetBool("type_red", red_type);
		anim_2.SetBool("type_red", red_type);



		Debug.Log(red_type);


		anim_1.SetFloat("speed", Mathf.Abs(rigidbody2D.velocity.x )); 
		anim_2.SetFloat("speed", Mathf.Abs(rigidbody2D.velocity.x) ); 

		//Debug.Log(state);
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



				anim_1.SetTrigger("alert");
				anim_2.SetTrigger("alert");



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
					anim_1.SetTrigger("alert");
					anim_2.SetTrigger("alert");
					



				}else{
					rigidbody2D.velocity = new Vector2(maxSpeed * direcao.x, rigidbody2D.velocity.y);
				}
			}
			break;
		case States.Perseguindo:


			if (1==0)
			{



			}
			else
				{


				if(triggerFollow){
					_chasingCount = 0;
				}
				if(_chasingCount>=1){
					_chasingCount = 0;
					direcao = -direcao;
					transform.Rotate(0,180,0);
					state = States.Descansando;
					anim_1.SetBool("exausted", true);
					anim_2.SetBool("exausted", true);
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

			}
			break;
		case States.Descansando:
			if(_chasingCount>=2){
				_chasingCount = 0;
				state = States.Procurando;
				anim_1.SetBool("exausted", false);
				anim_2.SetBool("exausted", false);
			}else{
				_chasingCount += Time.fixedDeltaTime;
			}
			break;
		}
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		
		if (!other.collider.CompareTag("Player")) return;
		
		if (!other.collider.GetComponent < PlayerController >()._character2D.Movement.isDead)
		{
			
			other.collider.GetComponent < PlayerController >()._character2D.Movement.isDead =true;
			other.collider.GetComponent < PlayerController >()._character2D.Movement.enabled = false;
			other.collider.GetComponent < PlayerController >()._character2D.Jump.enabled = false;
			other.collider.GetComponent < PlayerController >()._character2D.Movement.speed=0;
			
		}
		
		
		
		
		
	}


}


