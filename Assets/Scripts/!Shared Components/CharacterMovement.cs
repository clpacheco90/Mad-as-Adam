using UnityEngine;
using System.Collections;

public static class CharacterMovement{	
	//-----------------------------------------------------------------------------------------------------------------------------//	
    public static bool IsMovingHorizontal() {
        var h = Input.GetAxisRaw("Horizontal");
        return h != 0f;
    }	
    public static bool IsMovingHorizontal(out float h, bool abs = false) {
        h = Input.GetAxisRaw("Horizontal");
        return (abs) ? (Mathf.Abs(h) > 0.1f) : h != 0f;
    }
    //-----------------------------------------------------------------------------------------------------------------------------//	
    public static bool IsMovingVertical() {
        var v = Input.GetAxisRaw("Vertical");
        return v != 0f;
    }
    public static bool IsMovingVertical(out float v, bool abs = false) {
        v = Input.GetAxisRaw("Vertical");
        return (abs) ? (Mathf.Abs(v) > 0.1f) : v != 0f;

    }
    //-----------------------------------------------------------------------------------------------------------------------------//			
    public static bool IsMoving() {
        var h = Input.GetAxisRaw("Horizontal");
		var v = Input.GetAxisRaw ("Vertical");		
        return (Mathf.Abs(h) > 0.1f) || (Mathf.Abs (v) > 0.1f);
	}
    //-----------------------------------------------------------------------------------------------------------------------------//			
    private static bool IsTouchingCeiling(ControllerMovement movement) {
        return (movement.collisionFlags & CollisionFlags.CollidedAbove) != 0;
    }
    //-----------------------------------------------------------------------------------------------------------------------------//	
	private static bool JustBecameUngrounded(ControllerJumping jump) {
		return (Time.time < (jump.lastGroundedTime + jump.groundingTimeout) && jump.lastGroundedTime > jump.lastTime);
	}
    //-----------------------------------------------------------------------------------------------------------------------------//	
	private static float CalculateJumpVerticalSpeed (ControllerMovement movement,float targetJumpHeight) {
		// From the jump height and gravity we deduce the upwards speed 
		// for the character to reach at the apex.
		return Mathf.Sqrt (2 * targetJumpHeight * movement.gravity);
	}
    //-----------------------------------------------------------------------------------------------------------------------------//		
	private static void DidJump (ControllerMovement movement, ControllerJumping jump) {
		jump.jumping 			= true;
		jump.reachedApex 		= false;
		jump.lastTime 			= Time.time;
		jump.lastStartHeight 	= movement.transform.position.y;
		jump.lastButtonTime 	= -10;
		jump.touchedCeiling 	= false;
		jump.buttonReleased 	= false;
	}
    //-----------------------------------------------------------------------------------------------------------------------------//	
    public static void GravityMovementX(ControllerMovement movement, ControllerJumping jump, CharacterController controller) {
        if (!movement.enabled) return;
        var h = Input.GetAxisRaw("Horizontal");
        movement.isMoving = Mathf.Abs(h) > 0.1f;
        //Debug.Log(h);

        if (movement.isMoving) movement.direction = new Vector3(h, movement.direction.y, movement.direction.z);

        var curSmooth = 0.0f; // Smooth the speed based on the current target direction
        var targetSpeed = Mathf.Min(Mathf.Abs(h), 1.0f); // Choose target speed

        if (controller.isGrounded) {
            curSmooth = movement.speedSmoothing * Time.smoothDeltaTime;
            targetSpeed *= movement.runSpeed;
            movement.hangTime = 0.0f;
        } else {
            curSmooth = jump.speedSmoothing * Time.smoothDeltaTime;
            targetSpeed *= jump.jumpSpeed;
            movement.hangTime += Time.smoothDeltaTime;
        }
        movement.speed = Mathf.Lerp(movement.speed, targetSpeed, curSmooth);
    }
    //-----------------------------------------------------------------------------------------------------------------------------//	
    public static void GravityMovementY(ControllerMovement movement, ControllerJumping jump, CharacterController controller) {

        /*
         * Only Move the vertical direction
         * player get the same position of the object
         * If jump was pressed, apply gravity
         * 
         */
        var v = Input.GetAxisRaw("Vertical");

        movement.isClimbing = Mathf.Abs(v) > 0.1f;

        //Debug.Log(h);
        if (movement.isClimbing) movement.direction = new Vector3(movement.direction.x, v, movement.direction.z);
        var curSmooth = 0.0f; // Smooth the speed based on the current target direction
        var targetSpeed = Mathf.Min(Mathf.Abs(v), 1.0f); // Choose target speed

        curSmooth = movement.speedSmoothing * Time.smoothDeltaTime;
        targetSpeed *= movement.runSpeed;
        movement.hangTime = 0.0f;
        movement.speed = Mathf.Lerp(movement.speed, targetSpeed, curSmooth);
    }
    //-----------------------------------------------------------------------------------------------------------------------------//
    public static void RefreshMovement(ControllerMovement movement, ControllerJumping jump, CharacterController controller) {
        //var lastPosition = movement.transform.position; // Save lastPosition for velocity calculation.
        var currentMovementOffset = (movement.direction * movement.speed) + (new Vector3(0.0f, movement.verticalSpeed, 0.0f)); // Calculate actual motion
        currentMovementOffset *= Time.smoothDeltaTime; // We always want the movement to be framerate independent.  Multiplying by Time.smoothDeltaTime does this.
        movement.slideX = 0.0f;
        movement.collisionFlags = controller.Move(currentMovementOffset);
        //! movement.velocity = (movement.transform.position - lastPosition) / Time.smoothDeltaTime; NOT IN USE
    }
    //-----------------------------------------------------------------------------------------------------------------------------//	
    public static void KinematicMovementX(ControllerMovement movement, ControllerJumping jump, CharacterController controller) {
        var h = Input.GetAxisRaw("Horizontal");
        movement.isMoving = Mathf.Abs(h) > 0.1f;
        //Debug.Log(h);

        if (movement.isMoving) movement.direction = new Vector3(h, movement.direction.y, movement.direction.z);

        var curSmooth     = 0.0f; // Smooth the speed based on the current target direction
        var targetSpeed   = Mathf.Min(Mathf.Abs(h), 1.0f); // Choose target speed        
        curSmooth         = movement.speedSmoothing * Time.smoothDeltaTime;
        targetSpeed      *= movement.runSpeed;
        movement.hangTime = 0.0f;        
        movement.speed    = Mathf.Lerp(movement.speed, targetSpeed, curSmooth);
    }
    //-----------------------------------------------------------------------------------------------------------------------------//	
    public static void KinematicMovementY(ControllerMovement movement, ControllerJumping jump, CharacterController controller) {
        var v = Input.GetAxisRaw("Vertical");
        movement.isMoving = Mathf.Abs(v) > 0.1f;
        //Debug.Log(h);

        if (movement.isMoving) movement.direction = new Vector3(movement.direction.x, v, movement.direction.z);
        
        var curSmooth     = 0.0f; // Smooth the speed based on the current target direction
        var targetSpeed   = Mathf.Min(Mathf.Abs(v), 1.0f); // Choose target speed        
        curSmooth         = movement.speedSmoothing * Time.smoothDeltaTime;
        targetSpeed      *= movement.runSpeed;
        movement.hangTime = 0.0f;
        movement.speed    = Mathf.Lerp(movement.speed, targetSpeed, curSmooth);
    }
    //-----------------------------------------------------------------------------------------------------------------------------//	
    //public static void ApplySquatDown(ControllerMovement movement, ControllerJumping jump, CharacterController controller) {
    //    if (!movement.enabled) return;
    //    var v = Input.GetAxisRaw("Vertical");
    //    movement.isSquatDown = Mathf.Abs(v) > 0.1f;
    //    Debug.Log(v);
    //}
    //-----------------------------------------------------------------------------------------------------------------------------//	
	public static void ApplyGravity (ControllerMovement movement,ControllerJumping jump, CharacterController controller) {
		var jumpButton = Input.GetButton("Jump");		
		
		
        // When we reach the apex of the jump we send out a message		
        if (jump.jumping && !jump.reachedApex && movement.verticalSpeed <= 0.0) {
			jump.reachedApex = true;
			//? DidJump(movement,jump);
			//SendMessage ("DidJumpReachApex", SendMessageOptions.DontRequireReceiver);
		}
		
		// * When jumping up we don't apply gravity for some time when the user is holding the jump button
		//   This gives more control over jump height by pressing the button longer
		if (!jump.touchedCeiling && IsTouchingCeiling(movement)){
			jump.touchedCeiling = true; // store this so we don't allow extra power jump to continue after character hits ceiling.
		}
		if (!jumpButton){
			jump.buttonReleased = true;
		}
		
		var extraPowerJump = jump.jumping && movement.verticalSpeed > 0.0f && jumpButton && !jump.buttonReleased && movement.transform.position.y < jump.lastStartHeight + jump.extraHeight && !jump.touchedCeiling;		

		if (extraPowerJump){
			return;
		}else if (controller.isGrounded){
			movement.verticalSpeed = -movement.gravity * Time.smoothDeltaTime;
		}else{
			movement.verticalSpeed -= movement.gravity * Time.smoothDeltaTime;
		}
			
		// Make sure we don't fall any faster than maxFallSpeed.  This gives our character a terminal velocity.
		movement.verticalSpeed = Mathf.Max (movement.verticalSpeed, -movement.maxFallSpeed);
	}
    //-----------------------------------------------------------------------------------------------------------------------------//	
	public static void ApplyJumping (ControllerMovement movement,ControllerJumping jump, CharacterController controller) {
        if (Input.GetButtonDown("Jump")) jump.lastButtonTime = Time.time;
		
        // Prevent jumping too fast after each other
        if (jump.lastTime + jump.repeatTime > Time.time) return;
		
        var isGrounded = controller.isGrounded;	
		
        // Allow jumping slightly after the character leaves a ledge,
		// as long as a jump hasn't occurred since we became ungrounded.
		if (isGrounded || JustBecameUngrounded(jump)) {
            if(isGrounded){
				jump.lastGroundedTime = Time.time;
			}			
			// Jump
			// - Only when pressing the button down
			// - With a timeout so you can press the button slightly before landing	
			if (jump.enabled && Time.time < jump.lastButtonTime + jump.timeout) {
                movement.verticalSpeed = CalculateJumpVerticalSpeed (movement,jump.height);
				// If we're on a platform, add the platform's velocity (times 1.4)
				// to the character's velocity. We only do this if the platform
				// is traveling upward.
				if(movement.activePlatform){
					var apRb = movement.activePlatform.rigidbody;
					if(apRb){
                        var apRbY = movement.activePlatform.rigidbody.velocity.y;
						if(apRbY > 0.0f){
                            apRbY *= 1.4f;
							movement.verticalSpeed += apRbY;
						}
					}
				}
				DidJump(movement,jump);
			}
        }
    }
    //-----------------------------------------------------------------------------------------------------------------------------//	
    public static void ApplyRotation(ref ControllerMovement movement, ref float quaternionY, bool canControl) {
        if (!canControl) return;
        //if (!movement.canRotation) return;

        var h = Input.GetAxisRaw("Horizontal");
        var v = Input.GetAxisRaw("Vertical");
        if (Mathf.Abs(v) > 0.1f) return;
        if (h > 0.1f) {
            quaternionY = -45;
        } else if (h < -0.1f) {
            quaternionY = -135;
        }
        var lr = Quaternion.Euler(0, quaternionY, 0) * Quaternion.LookRotation(movement.direction);
        //Debug.Log(movement.direction);
        movement.transform.rotation = Quaternion.Slerp(movement.transform.rotation, lr, Time.smoothDeltaTime * movement.rotationSmoothing);
        //movement.transform.rotation = new Quaternion(0, movement.transform.rotation.y, 0, movement.transform.rotation.w);        
        //NGUIDebug.Log(movement.transform.rotation.ToString());
    }
    //-----------------------------------------------------------------------------------------------------------------------------//	
    public static void BulletMovement(Transform t) {
        //var v           = new Vector3(t.position.x, t.position.y, t.position.z);
        //var curSmooth   = 0.0f; // Smooth the speed based on the current target direction
        //var targetSpeed = 1.0f; // Choose target speed

        //curSmooth          = jump.speedSmoothing * Time.smoothDeltaTime;
        //targetSpeed       *= jump.jumpSpeed;
        //movement.hangTime += Time.smoothDeltaTime;
        //movement.speed = Mathf.Lerp(movement.speed, targetSpeed, curSmooth);
        //t.Translate();
    }
    //-----------------------------------------------------------------------------------------------------------------------------//	    

    public static void GravityMovementX(CharacterController2D c) {
        if (!c.Movement.enabled) return;
        var h = Input.GetAxisRaw("Horizontal");
        c.Movement.isMoving = Mathf.Abs(h) > 0.1f;
        //Debug.Log(h);

        if (c.Movement.isMoving) c.Movement.direction = new Vector3(h, 0, 0);

        var curSmooth = 0.0f; // Smooth the speed based on the current target direction
        var targetSpeed = Mathf.Min(Mathf.Abs(h), 1.0f); // Choose target speed

        if (c.isGrounded) {
            curSmooth = c.Movement.speedSmoothing * Time.smoothDeltaTime;
            targetSpeed *= c.Movement.runSpeed;
            c.Movement.hangTime = 0.0f;
        } else {
            curSmooth = c.Jump.speedSmoothing * Time.smoothDeltaTime;
            targetSpeed *= c.Jump.jumpSpeed;
            c.Movement.hangTime += Time.smoothDeltaTime;
        }
        c.Movement.speed = Mathf.Lerp(c.Movement.speed, targetSpeed, curSmooth);
    }
    //-----------------------------------------------------------------------------------------------------------------------------//	
    public static void GravityMovementY(CharacterController2D c) {

        /*
         * Only Move the vertical direction
         * player get the same position of the object
         * If jump was pressed, apply gravity
         * 
         */
        var v = Input.GetAxisRaw("Vertical");

        c.Movement.isClimbing = Mathf.Abs(v) > 0.1f;

        //Debug.Log(h);
        if (c.Movement.isClimbing) c.Movement.direction = new Vector3(0, v, 0);
        var curSmooth = 0.0f; // Smooth the speed based on the current target direction
        var targetSpeed = Mathf.Min(Mathf.Abs(v), 1.0f); // Choose target speed

        curSmooth = c.Movement.speedSmoothing * Time.smoothDeltaTime;
        targetSpeed *= c.Movement.runSpeed;
        c.Movement.hangTime = 0.0f;
        c.Movement.verticalSpeed = Mathf.Lerp(c.Movement.verticalSpeed, targetSpeed, curSmooth);
    }
    //-----------------------------------------------------------------------------------------------------------------------------//	
    public static void RefreshMovement(CharacterController2D c) {
        //var lastPosition          = c.Movement.transform.position; // Save lastPosition for velocity calculation.
        var currentMovementOffset   = (c.Movement.direction * (c.Movement.speed)) + (new Vector3(0.0f, c.Movement.verticalSpeed, 0.0f)); // Calculate actual motion
        currentMovementOffset      *= Time.smoothDeltaTime; // We always want the movement to be framerate independent.  Multiplying by Time.smoothDeltaTime does this.
        c.Movement.slideX           = 0.0f;
        c.Movement.collisionFlags   = CollisionFlags.CollidedBelow;
        c.Move(currentMovementOffset);
        //! movement.velocity     = (movement.transform.position - lastPosition) / Time.smoothDeltaTime; NOT IN USE
    }
    //-----------------------------------------------------------------------------------------------------------------------------//	
    public static void ApplyGravity(CharacterController2D c) {
        bool jumpButton;

#if UNITY_ANDROID
        jumpButton = (Input.touchCount > 0) ? true : false;
#endif
        jumpButton = Input.GetButton("Jump");

        // When we reach the apex of the jump we send out a message		
        if (c.Jump.jumping && !c.Jump.reachedApex && c.Movement.verticalSpeed <= 0.0) {
            c.Jump.reachedApex = true;
            //? DidJump(movement,jump);
            //SendMessage ("DidJumpReachApex", SendMessageOptions.DontRequireReceiver);
        }

        // * When jumping up we don't apply gravity for some time when the user is holding the jump button
        //   This gives more control over jump height by pressing the button longer
        if (!c.Jump.touchedCeiling && IsTouchingCeiling(c.Movement)) {
            c.Jump.touchedCeiling = true; // store this so we don't allow extra power jump to continue after character hits ceiling.
        }
        if (!jumpButton) {
            c.Jump.buttonReleased = true;
            //Debug.Log("Release");
        }

        var extraPowerJump = c.Jump.jumping && c.Movement.verticalSpeed > 0.0f && jumpButton && !c.Jump.buttonReleased && c.Movement.transform.position.y < c.Jump.lastStartHeight + c.Jump.extraHeight && !c.Jump.touchedCeiling;

        if (extraPowerJump) {
            Debug.Log("extraJump");
            return;
        } else if (c.isGrounded) {
            c.Movement.verticalSpeed = -c.Movement.gravity * Time.smoothDeltaTime;
        } else {
            //Debug.Log("Jump");
            c.Movement.verticalSpeed -= c.Movement.gravity * Time.smoothDeltaTime;
        }

        // Make sure we don't fall any faster than maxFallSpeed.  This gives our character a terminal velocity.
        c.Movement.verticalSpeed = Mathf.Max(c.Movement.verticalSpeed, -c.Movement.maxFallSpeed);
    }
    //-----------------------------------------------------------------------------------------------------------------------------//	
    public static void ApplyJumping(CharacterController2D c) {
    
        #if UNITY_ANDROID
        if (Input.touchCount > 0) {
            c.Jump.lastButtonTime = Time.time;
        }
        #endif
        if (Input.GetButtonDown("Jump")) c.Jump.lastButtonTime = Time.time;

        // Prevent jumping too fast after each other
        if (c.Jump.lastTime + c.Jump.repeatTime > Time.time) return;

        var isGrounded = c.isGrounded;

        // Allow jumping slightly after the character leaves a ledge,
        // as long as a jump hasn't occurred since we became ungrounded.
        if (isGrounded || JustBecameUngrounded(c.Jump)) {
            if (isGrounded) {
                c.Jump.lastGroundedTime = Time.time;
            }
            // Jump
            // - Only when pressing the button down
            // - With a timeout so you can press the button slightly before landing	
            if (c.Jump.enabled && Time.time < c.Jump.lastButtonTime + c.Jump.timeout) {
                c.Movement.verticalSpeed = CalculateJumpVerticalSpeed(c.Movement, c.Jump.height);
                // If we're on a platform, add the platform's velocity (times 1.4)
                // to the character's velocity. We only do this if the platform
                // is traveling upward.
                //if (c.Movement.activePlatform) {
                //    var apRb = c.Movement.activePlatform.rigidbody;
                //    if (apRb) {
                //        var apRbY = c.Movement.activePlatform.rigidbody.velocity.y;
                //        if (apRbY > 0.0f) {
                //            apRbY *= 1.4f;
                //            c.Movement.verticalSpeed += apRbY;
                //        }
                //    }
                //}
                DidJump(c.Movement, c.Jump);
            }
        }
    }
    //-----------------------------------------------------------------------------------------------------------------------------//
}
#region 2DController
//public static void GravityMovementX(CharacterController2D c) {
//    if (!c.Movement.enabled) return;
//    var h = Input.GetAxisRaw("Horizontal");
//    c.Movement.isMoving = Mathf.Abs(h) > 0.1f;
//    //Debug.Log(h);

//    if (c.Movement.isMoving) c.Movement.direction = new Vector3(h, 0, 0);

//    var curSmooth = 0.0f; // Smooth the speed based on the current target direction
//    var targetSpeed = Mathf.Min(Mathf.Abs(h), 1.0f); // Choose target speed

//    if (c.isGrounded) {
//        curSmooth = c.Movement.speedSmoothing * Time.smoothDeltaTime;
//        targetSpeed *= c.Movement.runSpeed;
//        c.Movement.hangTime = 0.0f;
//    } else {
//        curSmooth = c.Jump.speedSmoothing * Time.smoothDeltaTime;
//        targetSpeed *= c.Jump.jumpSpeed;
//        c.Movement.hangTime += Time.smoothDeltaTime;
//    }
//    c.Movement.speed = Mathf.Lerp(c.Movement.speed, targetSpeed, curSmooth);
//}
////-----------------------------------------------------------------------------------------------------------------------------//	
//public static void GravityMovementY(CharacterController2D c) {

//    /*
//     * Only Move the vertical direction
//     * player get the same position of the object
//     * If jump was pressed, apply gravity
//     * 
//     */
//    var v = Input.GetAxisRaw("Vertical");

//    c.Movement.isClimbing = Mathf.Abs(v) > 0.1f;

//    //Debug.Log(h);
//    if (c.Movement.isClimbing) c.Movement.direction = new Vector3(0, v, 0);
//    var curSmooth = 0.0f; // Smooth the speed based on the current target direction
//    var targetSpeed = Mathf.Min(Mathf.Abs(v), 1.0f); // Choose target speed

//    curSmooth = c.Movement.speedSmoothing * Time.smoothDeltaTime;
//    targetSpeed *= c.Movement.runSpeed;
//    c.Movement.hangTime = 0.0f;
//    c.Movement.verticalSpeed = Mathf.Lerp(c.Movement.verticalSpeed, targetSpeed, curSmooth);
//}
////-----------------------------------------------------------------------------------------------------------------------------//	
//public static void RefreshMovement(CharacterController2D c) {
//    //var lastPosition          = c.Movement.transform.position; // Save lastPosition for velocity calculation.
//    var currentMovementOffset = (c.Movement.direction * (c.Movement.speed)) + (new Vector3(0.0f, c.Movement.verticalSpeed, 0.0f)); // Calculate actual motion
//    currentMovementOffset *= Time.smoothDeltaTime; // We always want the movement to be framerate independent.  Multiplying by Time.smoothDeltaTime does this.
//    c.Movement.slideX = 0.0f;
//    c.Movement.collisionFlags = CollisionFlags.CollidedBelow;
//    c.Move(currentMovementOffset);
//    //! movement.velocity     = (movement.transform.position - lastPosition) / Time.smoothDeltaTime; NOT IN USE
//}
////-----------------------------------------------------------------------------------------------------------------------------//	
//public static void ApplyGravity(CharacterController2D c) {
//    var jumpButton = Input.GetButton("Jump");

//    // When we reach the apex of the jump we send out a message		
//    if (c.Jump.jumping && !c.Jump.reachedApex && c.Movement.verticalSpeed <= 0.0) {
//        c.Jump.reachedApex = true;
//        //? DidJump(movement,jump);
//        //SendMessage ("DidJumpReachApex", SendMessageOptions.DontRequireReceiver);
//    }

//    // * When jumping up we don't apply gravity for some time when the user is holding the jump button
//    //   This gives more control over jump height by pressing the button longer
//    if (!c.Jump.touchedCeiling && IsTouchingCeiling(c.Movement)) {
//        c.Jump.touchedCeiling = true; // store this so we don't allow extra power jump to continue after character hits ceiling.
//    }
//    if (!jumpButton) {
//        c.Jump.buttonReleased = true;
//    }

//    var extraPowerJump = c.Jump.jumping && c.Movement.verticalSpeed > 0.0f && jumpButton && !c.Jump.buttonReleased && c.Movement.transform.position.y < c.Jump.lastStartHeight + c.Jump.extraHeight && !c.Jump.touchedCeiling;

//    if (extraPowerJump) {
//        return;
//    } else if (c.isGrounded) {
//        c.Movement.verticalSpeed = -c.Movement.gravity * Time.smoothDeltaTime;
//    } else {
//        c.Movement.verticalSpeed -= c.Movement.gravity * Time.smoothDeltaTime;
//    }

//    // Make sure we don't fall any faster than maxFallSpeed.  This gives our character a terminal velocity.
//    c.Movement.verticalSpeed = Mathf.Max(c.Movement.verticalSpeed, -c.Movement.maxFallSpeed);
//}
////-----------------------------------------------------------------------------------------------------------------------------//	
//public static void ApplyJumping(CharacterController2D c) {
//        if (Input.GetButtonDown("Jump")) c.Jump.lastButtonTime = Time.time;

//        // Prevent jumping too fast after each other
//        if (c.Jump.lastTime + c.Jump.repeatTime > Time.time) return;

//        var isGrounded = c.isGrounded;

//        // Allow jumping slightly after the character leaves a ledge,
//        // as long as a jump hasn't occurred since we became ungrounded.
//        if (isGrounded || JustBecameUngrounded(c.Jump)) {
//            if (isGrounded) {
//                c.Jump.lastGroundedTime = Time.time;
//            }
//            // Jump
//            // - Only when pressing the button down
//            // - With a timeout so you can press the button slightly before landing	
//            if (c.Jump.enabled && Time.time < c.Jump.lastButtonTime + c.Jump.timeout) {
//                c.Movement.verticalSpeed = CalculateJumpVerticalSpeed(c.Movement, c.Jump.height);
//                // If we're on a platform, add the platform's velocity (times 1.4)
//                // to the character's velocity. We only do this if the platform
//                // is traveling upward.
//                if (c.Movement.activePlatform) {
//                    var apRb = c.Movement.activePlatform.rigidbody;
//                    if (apRb) {
//                        var apRbY = c.Movement.activePlatform.rigidbody.velocity.y;
//                        if (apRbY > 0.0f) {
//                            apRbY *= 1.4f;
//                            c.Movement.verticalSpeed += apRbY;
//                        }
//                    }
//                }
//                DidJump(c.Movement, c.Jump);
//            }
//        }
//    }
//    //-----------------------------------------------------------------------------------------------------------------------------//
#endregion    