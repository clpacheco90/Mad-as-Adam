using UnityEngine;
using System.Collections;

public class EndGameCollider : MonoBehaviour {

    public string AutoFade = "AutoFade";
    public string loadLevel = "Game 2";

	void OnTriggerEnter2D (Collider2D other) {
        if (!other.transform.parent.CompareTag("Player")) return;
        other.transform.parent.GetComponent<PlayerController>()._character2D.Movement.enabled = false;
        GameObject.Find(AutoFade).GetComponent<LoadLevel>()._autoFadeConfig.nextScene = loadLevel;
        GameObject.Find(AutoFade).GetComponent<LoadLevel>().enabled = true;

	}
}
