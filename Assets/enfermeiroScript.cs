using UnityEngine;
using System.Collections;

public enum States{Procurando, Desconfiado, Perseguindo};
public class enfermeiroScript : MonoBehaviour {

	public float areaRondaTam;
	public Vector2 posInicial;
	public float maxSpeed = 2.0f;
	public Vector2 direcao;
	private States state;
	public static bool triggerDesconfiada = false;
	public static bool triggerFollow = false;

	// Use this for initialization
	void Start () {
		state = States.Procurando;
		direcao = new Vector2 (1,0); 
	}
	
	// Update is called once per frame
	void Update () {
		switch (state) 
		{
		case States.Procurando:
			//Debug.Log ("procurando" + rigidbody2D.velocity);
			rigidbody2D.velocity = new Vector2(maxSpeed * direcao.x, rigidbody2D.velocity.y); 
			//Debug.Log ("procurando" + rigidbody2D.velocity);

			if(Mathf.Abs(posInicial.x - transform.position.x) >= areaRondaTam || Mathf.Abs(posInicial.x - transform.position.x) <= -areaRondaTam){
				direcao = -direcao; 
				Debug.Log (posInicial.x - transform.position.x);
			}
			if(triggerFollow && !GameObject.Find("Player").GetComponent<PlayerController>().isCrounch)
			{
				state = States.Perseguindo; 
			}
			else if(triggerFollow && !GameObject.Find("Player").GetComponent<PlayerController>().isCrounch){

			}

								break;

		case States.Desconfiado:break;
		case States.Perseguindo:break;
		}
	}
}
