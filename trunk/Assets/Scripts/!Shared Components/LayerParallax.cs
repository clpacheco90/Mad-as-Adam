using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider))]
public class LayerParallax : MonoBehaviour {
//-----------------------------------------------------------------------------------------------------------------------------//			
    public CharacterController _controller;
	public float _speedFactor;
	public bool _useTrigger;
	private Vector3 _velocity;
	private bool _startParallax;
//-----------------------------------------------------------------------------------------------------------------------------//			
    void Start() {
        _controller = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController>();
		this.gameObject.collider.isTrigger = true;
    }
//-----------------------------------------------------------------------------------------------------------------------------//			
	void Update(){
		if (_useTrigger && !_startParallax) return;
		_velocity.x = _controller.velocity.x * _speedFactor;
		transform.Translate(_velocity * Time.deltaTime);
	}
//-----------------------------------------------------------------------------------------------------------------------------//			
	void OnTriggerEnter(Collider other){
		if (!other.CompareTag(_controller.tag)) return;
		_startParallax = true;
	}
//-----------------------------------------------------------------------------------------------------------------------------//				
}
