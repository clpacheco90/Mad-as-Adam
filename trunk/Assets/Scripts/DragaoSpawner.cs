using UnityEngine;
using System.Collections;

public class DragaoSpawner : MonoBehaviour {

	public GameObject DragaoPrefab;
	public Vector3 spawnOffset;


	float nextCount = 0;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if((Mathf.Round(nextCount*10) == 0) || (Mathf.Round(nextCount*10) == 40)|| (Mathf.Round(nextCount*10) == 100)|| (Mathf.Round(nextCount*10) == 140) || (Mathf.Round(nextCount*10) == 180) || (Mathf.Round(nextCount*10) == 260)){
			float direcao = 1;

			GameObject dragao = GameObject.Instantiate(DragaoPrefab as Object,transform.position,Quaternion.identity) as GameObject;

			dragao.transform.position = new Vector3(50.0f,2.0f,0.0f);
			if(direcao == -1){
				dragao.transform.Rotate(0,180,0);
			}
			nextCount = Mathf.Round(nextCount*10);
			nextCount +=1;
			nextCount = nextCount / 10;

		}
		if(nextCount < 30.0f)
		{

			nextCount+= Time.deltaTime;
		}
		else
		{

			nextCount = 0;

		}

	}
	
	public void OnTriggerEnter2D(Collider2D other){
		//dar um nome melhor ao objeto que possui esse collider
		if(other.gameObject.name.Equals("sprite")){

		}
		
	}
	
	public void OnTriggerExit2D(Collider2D other){
		//dar um nome melhor ao objeto que possui esse collider
		if(other.gameObject.name.Equals("sprite")){

		}
	}
}
