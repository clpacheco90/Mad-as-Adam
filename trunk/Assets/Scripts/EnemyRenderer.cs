using UnityEngine;
using System.Collections;

public enum Domain{Real,Imaginary}
public class EnemyRenderer : MonoBehaviour {
	public Domain _domainType = Domain.Real;
	public Sprite _spriteImag;
	public Sprite _spriteReal;
	// Use this for initialization
	void Start () {
		//codigo teste para mover o inimigo no inicio
		rigidbody2D.AddForce(new Vector2(6.0f,0));
	}
	
	// Update is called once per frame
	void Update () {
		move();
	}

	void checkVision(){
		SpriteRenderer thisSpriteRenderer = renderer as SpriteRenderer;
		switch(_domainType){
		case Domain.Imaginary:
			thisSpriteRenderer.sprite = _spriteImag;
			//depois inserir um codigo decente para troca de sprites
			break;
		case Domain.Real:
			thisSpriteRenderer.sprite = _spriteReal;
			//depois inserir um codigo decente para troca de sprites
			break;
		default:
			break;
		}
	}

	public void setDomain(Domain newDomain){
		_domainType = newDomain;
		checkVision();
		//fazer mais coisas como fade in ou fade out
	}

	public void move(){
		float speed = 20.0f;
		if (transform.position.x < -10) {
			//turn around
			//transform.Rotate(new Vector3(0,180,0));
			//speed = -speed;
			//transform.Translate(new Vector3(0, -transform.position.y,0) );
			rigidbody2D.AddForce(new Vector2(speed,0));
		}
		if(transform.position.x > 16){
			rigidbody2D.AddForce(new Vector2(-speed,0));
		}
		//transform.Translate( new Vector3(speed*Time.deltaTime,0,0) );
	}
}
