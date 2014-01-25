using UnityEngine;
using System.Collections;

public class FixLoadLevel : MonoBehaviour {
//-----------------------------------------------------------------------------------------------------------------------------//			
    public float _waitForSeconds = 3f;
//-----------------------------------------------------------------------------------------------------------------------------//				
	void Start () {
        StartCoroutine(EnableLoadLevel());
	}
//-----------------------------------------------------------------------------------------------------------------------------//			
    IEnumerator EnableLoadLevel() {
        yield return new WaitForSeconds(_waitForSeconds);
        this.gameObject.GetComponent<LoadLevel>().enabled = true;
    }
//-----------------------------------------------------------------------------------------------------------------------------//			
}
