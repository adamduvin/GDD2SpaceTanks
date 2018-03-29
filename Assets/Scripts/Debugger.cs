using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debugger : MonoBehaviour {

    float move = 0.1f;
    Vector2 position;

    // Use this for initialization
    void Start () {
        position = transform.position;
    }
	
	// Update is called once per frame
	void Update () {

        position.x += move;
        transform.position = position;
        if(position.x > 9.0f || position.x < -9.0f)
        {
            move *= -1;
        }
	}
}
