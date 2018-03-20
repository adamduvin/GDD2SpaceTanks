using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleProperties : MonoBehaviour {

    public float mass;      // Mass

	// Use this for initialization
	void Start () {
        mass *= transform.localScale.x;
        mass /= 6;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
