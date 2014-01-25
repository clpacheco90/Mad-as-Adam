using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
    //-----------------------------------------------------------------------------------------------------------------------------//	
    public CharacterController2D _character2D = new CharacterController2D();

    //delegate void Controllers(ControllerMovement m, ControllerJumping j, CharacterController c);
    delegate void Controllers(CharacterController2D c);
	private Controllers _controllers;

	public bool isCrounch = false;
    //-----------------------------------------------------------------------------------------------------------------------------//	
	void Awake () {
        _character2D.StartControllers(this.gameObject);
        _controllers += CharacterMovement.GravityMovementX;
        //_controllers += CharacterMovement.GravityMovementY;
        _controllers += CharacterMovement.ApplyGravity;
        _controllers += CharacterMovement.ApplyJumping;
        _controllers += CharacterMovement.RefreshMovement;
	}
    //-----------------------------------------------------------------------------------------------------------------------------//	
	void Update () {

		//if(Input.GetAxis())

	
        if (_character2D.Movement.direction.sqrMagnitude > 0.01) _controllers += CharacterMovement.ApplyRotation;
        else _controllers -= CharacterMovement.ApplyRotation;
            //CharacterMovement.ApplyRotation(ref _character2D.Movement, ref _quaternionY, _attributes.canControl);
        //NGUIDebug.Log(_controllerMovement.direction.ToString());        
        //float v;
        //CharacterMovement.IsMovingVertical(out v, true);
        //Debug.Log(v);
        _character2D.UpdateControllers();		
	}
    //-----------------------------------------------------------------------------------------------------------------------------//	
	void FixedUpdate(){
        //_controllers(_character2D.Movement, _character2D.Jump, _character2D.Controller);
        
        _controllers(_character2D);
	}
    //-----------------------------------------------------------------------------------------------------------------------------//	

}
