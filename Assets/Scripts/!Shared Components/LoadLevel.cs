using UnityEngine;
using System.Collections;

public class LoadLevel : MonoBehaviour{
//-----------------------------------------------------------------------------------------------------------------------------//			
    public AutoFadeConfig _autoFadeConfig;	    
    private bool _load = false;
//-----------------------------------------------------------------------------------------------------------------------------//			
    void Start() {
        if (!_autoFadeConfig.clickEvent) _load = true;
    }
//-----------------------------------------------------------------------------------------------------------------------------//			
	void OnClick ()
	{
        if (!string.IsNullOrEmpty(_autoFadeConfig.nextScene))
		{
            AutoFade.LoadLevel(_autoFadeConfig.nextScene, _autoFadeConfig.fadeOutTime, _autoFadeConfig.fadeInTime, _autoFadeConfig.fadeColor);                        			
		}
	}
//-----------------------------------------------------------------------------------------------------------------------------//			
    void Update() {
        AutoFade.LoadLevel(_autoFadeConfig.nextScene, _autoFadeConfig.fadeOutTime, _autoFadeConfig.fadeInTime, _autoFadeConfig.fadeColor);
    }
//-----------------------------------------------------------------------------------------------------------------------------//			
    void Active() {
        OnClick();
        _load = false;
    }
//-----------------------------------------------------------------------------------------------------------------------------//			
}