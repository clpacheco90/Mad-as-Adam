using UnityEngine;
using System.Collections;

public class DragaoSummoner : MonoBehaviour {

    public GameObject DragaoPrefab;
    public Vector3 offset;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnTriggerEnter2D(Collider2D other) {
        Vector3 novaPos = transform.position+offset;
        novaPos.y = -6;
        novaPos.z = 0;
        Instantiate(DragaoPrefab,novaPos,Quaternion.identity);
        Destroy(gameObject);
    }
}
