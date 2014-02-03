using UnityEngine;
using System.Collections;

public class PlayerAnimations : MonoBehaviour 
{
	private PlayerController player;
	Animator anim;

	// Use this for initialization
	void Start () 
	{
		// The "player" object gets the Animator component from it's "sprite" child 
		anim = GetComponentInChildren<Animator>();

		player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

	}
	
	// Update is called once per frame
	void Update () 
	{


		if (player._character2D.Movement.isDead)
		{

			anim.SetBool("dead", true);

		}
		else
		{

			// Sets the speed parameter (for Animator) based on the current axis pressed
			anim.SetFloat("speed", Mathf.Abs(Input.GetAxisRaw("Horizontal")));
			//Debug.Log(Mathf.Abs(Input.GetAxisRaw("Horizontal")));

			// If the character is not grounded AND is jumping
			if (player._character2D.isGrounded == false && player._character2D.Jump.jumping)
			{
				anim.SetBool("jumping", true);
				//Debug.Log ("test1");
			}
			// If the player is grounded
			else if(player._character2D.isGrounded == true)
			{
				anim.SetBool("jumping", false);
				//Debug.Log ("test2");

				if (player._character2D.Movement.speed == 0)
				{

					anim.SetBool("stopped", false);
					//Debug.Log ("test2");

				}
			}
		}
	}
}
