using UnityEngine;
using System.Collections;

public class DoorCollider : MonoBehaviour {

    private PlayerController _player;
	Animator anim;
	//bool InMyState; // Variable to check if a specific animation is playing
	bool IsDone = false;

	// Use this for initialization
	void Start () {
		anim = GetComponentInChildren<Animator>();
	}
	
	// Update is called once per frame
	void Update() 
	{
		// If the animation "portaFechada" is not playing, the door is not closed
		if(!anim.GetCurrentAnimatorStateInfo(0).IsName("portaFechada"))
		{
			anim.SetBool("doorClosed", false);

			//Avoid any reload
			//InMyState = true;
			//Debug.Log("the door is not closed");
		}
		else
		{
			anim.SetBool("doorClosed", true);

			//InMyState = false;
			//You have just leaved your state!
			//Debug.Log("the door is closed");
		}

	}

	void OnTriggerEnter2D (Collider2D other) {
        if(!other.transform.parent.CompareTag("Player")) return;
		anim.SetBool("doorOpening", true);
		//Debug.Log("Entered Door Trigger");

        //_player = other.transform.parent.GetComponent<PlayerController>();
        //Debug.Log(other.transform.parent.name);
        //if (other.transform.parent.GetComponent<PlayerController>()._character2D.Movement.isSquatDown) {
        //    Debug.Log(" HERE");
        //    Physics2D.IgnoreLayerCollision(12, 8, true);
        //}
	}

    void OnTriggerExit2D (Collider2D other) {
        if (!other.transform.parent.CompareTag("Player")) return;
		anim.SetBool("doorOpening", false);
		//Debug.Log("Exited Door Trigger");

        //other.transform.FindChild("sprite").collider2D.enabled = true;
        //other.transform.parent.rigidbody2D.isKinematic = false;
    }
}
