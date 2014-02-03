using UnityEngine;
using System.Collections;

public class AutoFade : MonoBehaviour {
//-----------------------------------------------------------------------------------------------------------------------------//			
    private static AutoFade _instance = null;
    private Material _material        = null;
    private string _levelName         = "";
    private int _levelIndex           = 0;
    private bool _fading              = false;
    private AsyncOperation _async;
//-----------------------------------------------------------------------------------------------------------------------------//			
    public static bool Fading {
        get { return Instance._fading; }
    }
    private static AutoFade Instance {
        get {
            if (_instance == null) {
                _instance = (new GameObject("AutoFade")).AddComponent<AutoFade>();
            }
            return _instance;
        }
    }
//-----------------------------------------------------------------------------------------------------------------------------//			    
    private void Awake() {
        DontDestroyOnLoad(this);
        _instance = this;
        _material = new Material("Shader \"Plane/No zTest\" { SubShader { Pass { Blend SrcAlpha OneMinusSrcAlpha ZWrite Off Cull Off Fog { Mode Off } BindChannels { Bind \"Color\",color } } } }");
    }
//-----------------------------------------------------------------------------------------------------------------------------//			
    private void DrawQuad(Color color, float alpha) {
        color.a = alpha;
        _material.SetPass(0);
        GL.Color(color);
        GL.PushMatrix();
        GL.LoadOrtho();
        GL.Begin(GL.QUADS);
        GL.Vertex3(0, 0, -1);
        GL.Vertex3(0, 1, -1);
        GL.Vertex3(1, 1, -1);
        GL.Vertex3(1, 0, -1);
        GL.End();
        GL.PopMatrix();
    }
//-----------------------------------------------------------------------------------------------------------------------------//			
    private IEnumerator Fade(float fadeOutTime, float fadeInTime, Color color, bool asyncLevel = false) {
        float t = 0.0f;
        while (t < 1.0f) {
            yield return new WaitForEndOfFrame();
            t = Mathf.Clamp01(t + Time.deltaTime / fadeOutTime);
            DrawQuad(color, t);
        }
        if (_levelName != ""){
            if (asyncLevel) _async = (AsyncOperation)Application.LoadLevelAsync(_levelName);
            else Application.LoadLevel(_levelName);
        }
        else{
            if (asyncLevel) _async = (AsyncOperation)Application.LoadLevelAsync(_levelIndex);
            else Application.LoadLevel(_levelIndex);       
        }

        while (t > 0.0f) {
            yield return new WaitForEndOfFrame();
            t = Mathf.Clamp01(t - Time.deltaTime / fadeInTime);
            DrawQuad(color, t);
        }
        _fading = false;
    }
//-----------------------------------------------------------------------------------------------------------------------------//			
    private void StartFade(float fadeOutTime, float fadeInTime, Color color, bool asyncLevel = false) {
        _fading = true;
        StartCoroutine(Fade(fadeOutTime, fadeInTime, color, asyncLevel));
    }
//-----------------------------------------------------------------------------------------------------------------------------//			
    public static void LoadLevel(string levelName, float fadeOutTime, float fadeInTime, Color color, bool asyncLevel = false) {
        if (Fading) return;
        Instance._levelName = levelName;
        Instance.StartFade(fadeOutTime, fadeInTime, color, asyncLevel);
    }
//-----------------------------------------------------------------------------------------------------------------------------//			
    public static void LoadLevel(int levelIndex, float fadeOutTime, float fadeInTime, Color color) {
        if (Fading) return;
        Instance._levelName = "";
        Instance._levelIndex = levelIndex;
        Instance.StartFade(fadeOutTime, fadeInTime, color);
    }
//-----------------------------------------------------------------------------------------------------------------------------//			
    public static void LoadLevel(AutoFadeConfig atfc) {
        if (Fading) return;
        Instance._levelName = atfc.nextScene;
        Instance.StartFade(atfc.fadeOutTime, atfc.fadeInTime, atfc.fadeColor);
    }
//-----------------------------------------------------------------------------------------------------------------------------//			
    IEnumerator LoadingScene(string levelName) {
        _async = (AsyncOperation)Application.LoadLevelAsync(levelName);
        Debug.Log(_async.progress);
        if (_async.isDone && _async.progress >= 0.9f) {
            Debug.LogWarning(_async.progress);
            yield return _async;            
        }
    }
//-----------------------------------------------------------------------------------------------------------------------------//			
    IEnumerator LoadingScene(int levelIndex) {
        _async = (AsyncOperation)Application.LoadLevelAsync(levelIndex);
        Debug.Log(_async.progress);
        if (_async.isDone && _async.progress >= 0.9f) {
            Debug.LogWarning(_async.progress);
            yield return _async;
        }
    }
//-----------------------------------------------------------------------------------------------------------------------------//			
}
