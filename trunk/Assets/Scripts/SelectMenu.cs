using UnityEngine;
using System.Collections;

public class SelectMenu : MonoBehaviour {

    public Color[] _colors = new Color[2];
    public GameObject[] mainMenu = new GameObject[2]; 
    float v = 0;
    public string scenePlay;
    public string sceneCredits;

    enum MainMenu {
        Play,Credits
    }
    private MainMenu main = MainMenu.Play;

	// Use this for initialization
	void Start () {
        mainMenu[0].GetComponent<SpriteRenderer>().color = _colors[0];
        mainMenu[1].GetComponent<SpriteRenderer>().color = _colors[1];        
	}
	
	// Update is called once per frame
	void Update () {
        
        CharacterMovement.IsMovingVertical(out v);
        if (v > 0.1f) {
            mainMenu[0].GetComponent<SpriteRenderer>().color = _colors[0];
            mainMenu[1].GetComponent<SpriteRenderer>().color = _colors[1];
            main = MainMenu.Play;
        } else if (v < -0.1f) {
            mainMenu[0].GetComponent<SpriteRenderer>().color = _colors[1];
            mainMenu[1].GetComponent<SpriteRenderer>().color = _colors[0];
            main = MainMenu.Credits;
        }
        
        switch (main) {
            case MainMenu.Play:
                GameObject.Find("AutoFade").GetComponent<LoadLevel>()._autoFadeConfig.nextScene = scenePlay;
                if ((!Input.GetButtonDown("Jump"))) return;
                GameObject.Find("AutoFade").GetComponent<LoadLevel>().enabled = true;
                break;
            case MainMenu.Credits:
                GameObject.Find("AutoFade").GetComponent<LoadLevel>()._autoFadeConfig.nextScene = sceneCredits;
                if ((!Input.GetButtonDown("Jump"))) return;
                GameObject.Find("AutoFade").GetComponent<LoadLevel>().enabled = true;
                break;         
        }

	}
}
