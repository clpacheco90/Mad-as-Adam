using UnityEngine;
using System.Collections;

public enum States{Procurando, Desconfiado, Perseguindo};
public class enfermeiroScript : MonoBehaviour {

	public float areaRondaTam;
	public Vector2 posInicial;
	public float maxSpeed = 10.0f;
	public Vector2 direcao = new Vector2 (1,0); 
	private States state;
	public static bool triggerDesconfiada = false;
	public static bool triggerFollow = false;

	// Use this for initialization
	void Start () {
		state = States.Procurando;
		direcao = new Vector2 (1,0); 
	}
	
	// Update is called once per frame
	void FixedUpdate () {
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
			}
			if(triggerFollow && !GameObject.Find("Player").GetComponent<PlayerController>().isCrounch)
			{
				state = States.Perseguindo; 
			}
			else if(triggerFollow && !GameObject.Find("Player").GetComponent<PlayerController>().isCrounch){
				
			}
			rigidbody2D.velocity = new Vector2(maxSpeed * direcao.x, rigidbody2D.velocity.y); 
			break;
			
		case States.Desconfiado:break;
		case States.Perseguindo:break;
		}
	}
}
