using UnityEngine;
using System.Collections;

public class AlmaSpawner : MonoBehaviour {
    float _nextAlma = 0;
	public float maxWidth;
	public GameObject AlmaPrefab;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(_nextAlma>=3.0f){
			_nextAlma = 0;
			float novoX = Random.Range(-maxWidth,maxWidth);
			Vector3 novaPosicao = new Vector3(transform.position.x+novoX,transform.position.y,-3);
			Instantiate(AlmaPrefab,novaPosicao,Quaternion.identity);
		}else{
			_nextAlma+= Time.deltaTime;
		}
	}
}
