using UnityEngine;
using System.Collections;
/// <summary>
/// Quits the application upon pressing the appropriate button
/// </summary>
public class quit : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKey(KeyCode.Escape)){
			Application.Quit();
		}
	}

}
