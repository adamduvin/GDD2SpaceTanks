using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Player1Script : MonoBehaviour {
    public GameObject bullet;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        var r2d = GetComponent("Rigidbody2D");
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(bullet,transform.position,Quaternion.identity);
        }

    }
}
