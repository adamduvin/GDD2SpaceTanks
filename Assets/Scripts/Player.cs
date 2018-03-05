using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public bool turn;               // True if it's this player's turn
    private Vector2 position;       // Player's position
    private Vector2 velocity;       // Player's velocity
    private Vector2 acceleration;   // Acceleration due to forces
    public Vector2 direction;       // Direction player is facing
    private float friction;         // Friction coeficient
    private float mass;             // Player's mass
    private float maxForce;         // Maximum applyable force
    private float maxVelocity;      // Maximum possible speed
    public Camera mainCamera;       // Main scene camera

	// Use this for initialization
	void Start () {
        position = transform.position;
        velocity = Vector2.zero;
        acceleration = Vector2.zero;
        direction = transform.up.normalized;
        friction = 0.5f;
        mass = 1.0f;
        maxForce = 1.0f;
        maxVelocity = 5.0f;
	}
	
	// Update is called once per frame
	void Update () {
        Rotate();
        // Applies forward thrust if 'w' is held down
        if (Input.GetKey(KeyCode.W))
        {
            ApplyMovement();
        }
        // Applies friction if the player lets go of 'w'
        else
        {
            if (velocity.magnitude > 0.1f)
            {
                ApplyFriction();
            }
            else velocity = Vector2.zero;
        }
        Move();
	}

    // Calculates the new velocity and applies it to the position
    void Move()
    {
        velocity += acceleration;
        velocity.Normalize();
        velocity *= maxVelocity;
        velocity *= Time.deltaTime;
        position += velocity;
        transform.position = position;

        acceleration = Vector2.zero;
        position = transform.position;
    }

    // Applies forward thrust force
    void ApplyMovement()
    {
        Vector2 movementForce = direction;
        movementForce *= maxForce;
        ApplyForce(movementForce);
    }

    // Applies friction force
    void ApplyFriction()
    {
        Vector2 frictionForce = -velocity * friction;
        ApplyForce(frictionForce);
    }

    // Rotates the player according to mouse location
    void Rotate()
    {
        direction = (Vector2)mainCamera.ScreenToWorldPoint((Vector2)Input.mousePosition) - position;
        direction.Normalize();
        //Quaternion rotation = new Quaternion(direction.x, direction.y, 0.0f, 0.0f);
        transform.up = (Vector3)direction;
        //transform.Rotate(new Vector3(0.0f, 0.0f, Input.GetAxis("Horiontal")) - (Vector3)(newDirection - direction));
        //direction = newDirection;
    }

    // Applies force to acceleration
    void ApplyForce(Vector2 force)
    {
        acceleration = force / mass;
    }

}
