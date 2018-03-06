using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class startButton : MonoBehaviour {

	public Button start;

	// Use this for initialization
	void Start () 
	{
		Button btn = start.GetComponent<Button> ();
		btn.onClick.AddListener (StartGame);
	}
	
	// Update is called once per frame
	void Update () 
	{

	}

	void StartGame()
	{
		Application.LoadLevel ("game");
		Debug.Log ("start");
	}
}
