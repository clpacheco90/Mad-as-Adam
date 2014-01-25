using UnityEngine;
using System.Collections;

[System.Serializable]
public class CharacterController2D {
    //-----------------------------------------------------------------------------------------------------------------------------//			
    public ControllerMovement Movement    = new ControllerMovement();
    public ControllerJumping Jump         = new ControllerJumping();
    //public CharacterController Controller = new CharacterController();
    
    public bool isGrounded;
    [HideInInspector]public Transform GroundCheck;
    [HideInInspector]public GameObject PlayerObject;
    //-----------------------------------------------------------------------------------------------------------------------------//
    public void StartControllers(GameObject playerObject){
        this.PlayerObject       = playerObject;
        this.Movement.transform = this.PlayerObject.transform;
        this.Movement.direction = Vector3.zero;
        //this.Controller         = this.PlayerObject.GetComponent<CharacterController>();
    }
    //-----------------------------------------------------------------------------------------------------------------------------//
    public void UpdateControllers() {
        this.GroundCheck = this.PlayerObject.transform.FindChild("groundCheck");
        //this.isGrounded  = Physics2D.Linecast(this.PlayerObject.transform.position, this.GroundCheck.position, 1 << LayerMask.NameToLayer("Ground"));
    }
    //-----------------------------------------------------------------------------------------------------------------------------//
    public void Move(Vector3 pos) {
        this.PlayerObject.transform.position += pos;
    }
}
