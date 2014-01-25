using UnityEngine;
using System.Collections;

public class CameraMoveAt : MonoBehaviour {
//-----------------------------------------------------------------------------------------------------------------------------//				
	public CameraConstrainsts _constrainsts;
	public float _offsetX;	
	public float _offsetY;	
	public float _smoothTime;
	public Transform _gameObj;
	private float _h;
	private float _v;	
	private Camera _cam;
	private Vector3 _velocity = Vector3.zero;
    private float _smoothDampX;
    private float _smoothDampY;
//-----------------------------------------------------------------------------------------------------------------------------//					
	void Start () {		
		_cam = this.gameObject.camera;
		//Debug.Log(_cam.transform.position);
	}
//-----------------------------------------------------------------------------------------------------------------------------//			
	void LateUpdate () {
        CameraSettings();        
	}
//-----------------------------------------------------------------------------------------------------------------------------//			
    void CameraSettings() {
        CharacterMovement.IsMovingHorizontal(out _h,true);
        //CharacterMovement.IsMovingVertical(out _v);
        _smoothDampX = Mathf.SmoothDamp(_cam.transform.position.x,_gameObj.transform.position.x + _offsetX, ref _velocity.x, _smoothTime);
        //_smoothDampY = _gameObj.transform.position.y + _offsetY;
        _smoothDampY = Mathf.SmoothDamp(_cam.transform.position.y,_gameObj.transform.position.y + _offsetY, ref _velocity.y, _smoothTime);

        if (_constrainsts.freezeX && _constrainsts.freezeY) { // Frezze
            _smoothDampY = _cam.transform.position.y;
            _smoothDampX = _cam.transform.position.x;
        } else if (!_constrainsts.freezeX && _constrainsts.freezeY) { // Only X            
            _smoothDampY = _cam.transform.position.y;
        } else if (_constrainsts.freezeX && !_constrainsts.freezeY) { // Only Y
            _smoothDampX = _cam.transform.position.x;
        }
        _cam.transform.position = new Vector3(_smoothDampX, _smoothDampY, _cam.transform.position.z);
    }
//-----------------------------------------------------------------------------------------------------------------------------//				
	public void UpdateConstraints(CameraConstrainsts constraints){
		_constrainsts = constraints;
        //NGUIDebug.Log(_constrainsts.ToString());
	}
//-----------------------------------------------------------------------------------------------------------------------------//			
}
