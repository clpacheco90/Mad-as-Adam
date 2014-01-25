using UnityEngine;
using System.Collections;

public class FantasmaSpawner : MonoBehaviour {

	public GameObject FantasmaPrefab;
	public Vector3 spawnOffset;
	public bool isSpawning = false;
	float nextCount = 0;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(isSpawning){
			if(nextCount >= 2.0f){
				float direcao = Random.Range(0,2)==0?1:-1;
				Debug.Log(direcao);
				GameObject fantasma = GameObject.Instantiate(FantasmaPrefab as Object,transform.position+spawnOffset,Quaternion.identity) as GameObject;
				fantasma.GetComponent<FantasmaBehaviour>().direcao = direcao;
				fantasma.transform.position = new Vector3(fantasma.transform.position.x*-direcao,fantasma.transform.position.y,fantasma.transform.position.z);
				if(direcao == -1){
					fantasma.transform.Rotate(0,180,0);
				}
				nextCount = 0;
			}else{
				nextCount+= Time.deltaTime;
			}
		}
	}

	public void OnTriggerEnter2D(Collider2D other){
		//dar um nome melhor ao objeto que possui esse collider
		if(other.gameObject.name.Equals("sprite")){
			isSpawning = true;
		}
		
	}
	
	public void OnTriggerExit2D(Collider2D other){
		//dar um nome melhor ao objeto que possui esse collider
		if(other.gameObject.name.Equals("sprite")){
			isSpawning = false;
		}
	}
}
