using UnityEngine;
using System.Collections;

public class SquatDownCollider : MonoBehaviour {

    private PlayerController _player;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void OnTriggerStay2D (Collider2D other) {
        if(!other.transform.parent.CompareTag("Player")) return;
        _player = other.transform.parent.GetComponent<PlayerController>();
        Debug.Log(other.transform.parent.name);
        if (other.transform.parent.GetComponent<PlayerController>()._character2D.Movement.isSquatDown) {
            Debug.Log(" HERE");
            Physics2D.IgnoreLayerCollision(12, 8, true);
        }
	}

    void OnTriggerExit2D (Collider2D other) {
        if (!other.transform.parent.CompareTag("Player")) return;
        other.transform.FindChild("sprite").collider2D.enabled = true;
        other.transform.parent.rigidbody2D.isKinematic = false;
    }
}
