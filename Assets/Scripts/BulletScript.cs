﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour {


    public Vector2 speed;
    public Vector2 direction;
    public float mass;
    public float maxSpeed;

    private Vector2 movement;
    private Rigidbody2D rigidbodyComponent;
    public int damage = 1;
    public bool isEnemyPlayerOneShot = false;
    public bool isEnemyPlayerTwoShot = false;

    public GameObject manager;
    public GravityManager gravMngr;

    // Use this for initialization
    void Start () {
        speed = new Vector2(1, 1);
        speed = speed.normalized;
        direction = new Vector2(1, 1);
        direction = direction.normalized;
        manager = GameObject.Find("GameManager");
        gravMngr = manager.GetComponent<GravityManager>();
        gravMngr.AssignProjectile(gameObject);
        mass = 1.0f;
        maxSpeed = 15.0f;

    }
	
	// Update is called once per frame
	void Update () {

        // 2 - Movement
        movement = new Vector2(
          speed.x * direction.x,
          speed.y * direction.y);

        

        if (rigidbodyComponent == null) rigidbodyComponent = GetComponent<Rigidbody2D>();

        movement += gravMngr.MoveProjectile();      // Adds gravity
        movement = movement.normalized;     // Normalize velocity vector
        movement *= maxSpeed;   // Multiply velocity by max speed to get movement speed

        // Apply movement to the rigidbody
        rigidbodyComponent.velocity = movement;

        direction = movement.normalized;    // Set direction as normal of velocity so it continues to move in the same direction
    }

  

    private void OnBecameInvisible()
    {
        gravMngr.NullProjectile();
        Destroy(gameObject);
    }
}
