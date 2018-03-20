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
    public GameObject bullet;       // Bullet prefab
    public GameObject bulletObject;
    public GameObject manager;

    public float speed;
    public Rigidbody2D rb2d;
    public bool IsKeyEnabled_Space { get; set; }




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
        bulletObject = null;
        manager = GameObject.Find("GameManager");


        rb2d = GetComponent<Rigidbody2D>();
        IsKeyEnabled_Space = true;
    }
	
	// Update is called once per frame
	void FixedUpdate () {



        if (IsKeyEnabled_Space)
        {




            if (turn)
            {

                /*
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
                */
                //float moveHorizontal = Input.GetAxis("Horizontal");
                float moveVertical = Input.GetAxis("Vertical");
                //float rotateHorizontal = Input.GetAxis("MouseX");
                // float rotateVertical = Input.GetAxis("MouseY");
                //direction = new Vector2 (rotateHorizontal,rotateVertical);
                Rotate();
                transform.Translate(0, moveVertical * speed * Time.deltaTime, 0);
                //Vector2 movement = new Vector2(moveHorizontal,moveVertical);
                // rb2d.AddForce(movement*speed*Time.deltaTime);
                var r2d = GetComponent("Rigidbody2D");
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    IsKeyEnabled_Space = false;
                    bulletObject = Instantiate(bullet, transform.position, Quaternion.identity);
                    bulletObject.GetComponent<BulletScript>().player = gameObject;

                }

            }










            /*
            // Here the position of the player is clamped into the boundaries
            transform.position = (new Vector3(
                Mathf.Clamp(transform.position.x, -7.5f, 7.5f),
                Mathf.Clamp(transform.position.y, -4.5f, 4.5f),
                transform.position.z)
            );
            */









        }
    }
    void Rotate()
    {
        direction = (Vector2)mainCamera.ScreenToWorldPoint((Vector2)Input.mousePosition) - position;
        direction.Normalize();
        transform.up = (Vector3)direction;
    }

    /*

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
        transform.up = (Vector3)direction;
    }

    // Applies force to acceleration
    void ApplyForce(Vector2 force)
    {
        acceleration = force / mass;
    }
    */
}
