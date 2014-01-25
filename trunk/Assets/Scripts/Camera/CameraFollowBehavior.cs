using UnityEngine;
using System.Collections;

public class CameraFollowBehavior : MonoBehaviour {

    public Camera _cam;
    public float _playerOffsetX = 1;
    public float _playerOffsetY;
    public bool _cameraSide;
    private float _cameraSideValue = 1;
    private GameObject _player;

    void Awake() {
        if (_cameraSide) _cameraSideValue = -1;
        _player = GameObject.Find("Player");
        CameraSettings();
    }

	void FixedUpdate(){
		CameraSettings();
	}

	private void CameraSettings(){
        this.transform.position = new Vector3(_player.transform.position.x * _playerOffsetX, /*_player.transform.position.y +*/ _playerOffsetY, this.transform.position.z);               
        float vertical_size = _cam.orthographicSize;
		float horizontal_size = vertical_size * _cam.aspect;        
        transform.Translate(horizontal_size * _cameraSideValue, 0, 0);
	}
}
