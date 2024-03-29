﻿using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
    //-----------------------------------------------------------------------------------------------------------------------------//	
    public CharacterController2D _character2D = new CharacterController2D();

    //delegate void Controllers(ControllerMovement m, ControllerJumping j, CharacterController c);
    delegate void Controllers(CharacterController2D c);
	private Controllers _controllers;

    //-----------------------------------------------------------------------------------------------------------------------------//	
	void Awake () {
        _character2D.StartControllers(this.gameObject);
        _controllers += CharacterMovement.GravityMovementX;
        _controllers += CharacterMovement.ApplySquatDown;
        _controllers += CharacterMovement.ApplyGravity;
        _controllers += CharacterMovement.ApplyJumping;
        _controllers += CharacterMovement.RefreshMovement;
	}
    //-----------------------------------------------------------------------------------------------------------------------------//	
	void Update () {		
        if(CharacterMovement.IsMoving()) CharacterMovement.ApplyRotation(_character2D);
        _character2D.UpdateControllers();	

	}
    //-----------------------------------------------------------------------------------------------------------------------------//	
	void FixedUpdate(){
        _controllers(_character2D);
	}
    //-----------------------------------------------------------------------------------------------------------------------------//	

		

}
