using UnityEngine;
using System.Collections;

public class GameOverDude : MonoBehaviour {

    public string GameoverScene;
    private PlayerController p;
    private bool gluglu;
	// Use this for initialization
	void Start () {
        p = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
	}
	
	// Update is called once per frame
	void Update () {
        if (!p._character2D.Movement.isDead) return;
        if (!gluglu) {
            GameObject.Find("AutoFadeLoad").GetComponent<LoadLevel>()._autoFadeConfig.nextScene = GameoverScene;
            GameObject.Find("AutoFadeLoad").GetComponent<LoadLevel>().enabled = true;
            gluglu = true;
        }
	}
}
