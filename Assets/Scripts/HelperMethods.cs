using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//It is common to create a class to contain all of your
//extension methods. This class must be static.
public static class HelperMethods
{
	//Even though they are used like normal methods, extension
	//methods must be declared static. Notice that the first
	//parameter has the 'this' keyword followed by a GameObject
	//variable. This variable denotes which class the extension
	//method becomes a part of.
	public static List<GameObject> GetChildren(this GameObject go)
	{
		List<GameObject> children = new List<GameObject>();
		foreach (Transform tran in go.transform)
		{
			children.Add(tran.gameObject);
		}
		return children;
	}

	// Helper function to find game objects with a specific layer (instead of a tag)
	public static List<GameObject> FindGameObjectsWithLayer (this GameObject go, int layerToFind)
	{
		// Check all game objects from the scene
		object[] obj = GameObject.FindObjectsOfType(typeof (GameObject));

		List<GameObject> goList = new List<GameObject>(); 

		foreach (object o in obj)
		{
			GameObject g = (GameObject) o;
			//Debug.Log(g.name);

			// If a game object with the specified layer was found, add it in the output list (goList)
			if (g.layer == layerToFind) 
			{
				goList.Add(g);
			}
		}

		// No game object was added to the output list, returns null
		if (goList.Count == 0) 
		{
			//Debug.Log ("returned null");
			return null;
		}

		//Debug.Log ("returned goList");
		return goList;
	
	}
}

//OBS.: Para chamar "Extension Methods" (exemplo)
//void Start () {
//Notice how you pass no parameter into this
//extension method even though you had one in the
//method declaration. The transform object that
//this method is called from automatically gets
//passed in as the first parameter.
//List<GameObject> children =	gameObject.GetChildren;
//transform.ResetTransformation(); (apenas outro exemplo de chamada, de outro Extension Method)
//}