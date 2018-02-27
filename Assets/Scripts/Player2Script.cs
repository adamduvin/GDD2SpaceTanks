using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2Script : MonoBehaviour {
    public GameObject bullet;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        var r2d = GetComponent("Rigidbody2D");
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            Instantiate(bullet, transform.position, Quaternion.identity);
        }

    }


}
