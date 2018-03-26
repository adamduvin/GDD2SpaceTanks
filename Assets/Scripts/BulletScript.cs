﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour {

    public GameObject player;       // Player who fired the shot
    //public float speed;
    public Vector2 direction;
    public float mass;
    public float maxSpeed;

    private Vector2 movement;
    private Rigidbody2D rigidbodyComponent;
    public int damage = 1;
    public bool isEnemyPlayerOneShot = false;
    public bool isEnemyPlayerTwoShot = false;
    public bool hit = false;

    public GameObject manager;
    public GravityManager gravMngr;
    private bool check;


    // Use this for initialization
    void Start () {
        //speed = 1.0f;
        //speed = speed.normalized;
        manager = GameObject.Find("GameManager");
        gravMngr = manager.GetComponent<GravityManager>();
        gravMngr.AssignProjectile(gameObject);
        player = manager.GetComponent<GameManager>().currentPlayer;
        direction = player.GetComponent<Player>().direction;
        direction = direction.normalized;
        mass = 1.0f;
        maxSpeed = 15.0f;
        check = false;

    }
	
	// Update is called once per frame
	void Update () {

        // 2 - Movement
        movement = direction * maxSpeed;
        

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
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        gravMngr.NullProjectile();
        Destroy(gameObject);
        /*
        if (GameObject.FindGameObjectWithTag("Player1") != null || GameObject.FindGameObjectWithTag("Player2") != null)
        {
        }
        */
    
            manager.GetComponent<GameManager>().SwitchTurn();
       

            player.GetComponent<Player>().IsKeyEnabled_Space = true;
    }
}
