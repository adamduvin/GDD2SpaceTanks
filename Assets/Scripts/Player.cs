using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public bool turn;               // True if it's this player's turn
    private Vector2 position;       // Player's position
    public Vector2 velocity;       // Player's velocity
    public Vector2 acceleration;   // Acceleration due to forces
    public Vector2 direction;       // Direction player is facing
    private float friction;         // Friction coeficient
    private float mass;             // Player's mass
    private float maxForce;         // Maximum applyable force
    private float maxVelocity;      // Maximum possible speed
    public Camera mainCamera;       // Main scene camera
    public GameObject bullet;       // Bullet prefab
    public GameObject bulletObject; // Reference to bullet object
    public float movementLimit;     // How much farther player can move in their turn, decreases based on time in seconds
    public GameObject manager;
    public int hp;

    public float speed;
    public Rigidbody2D rb2d;
    public bool IsKeyEnabled_Space { get; set; }

    // Use this for initialization
    void Start () {
        position = transform.position;
        velocity = Vector2.zero;
        acceleration = Vector2.zero;
        direction = transform.up.normalized;
        friction = 1.0f;
        mass = 1.0f;
        maxForce = 10.0f;
        maxVelocity = 10.0f;
        bulletObject = null;
        movementLimit = 1.0f;
        manager = GameObject.Find("GameManager");
        hp = 3;


        rb2d = GetComponent<Rigidbody2D>();
        IsKeyEnabled_Space = true;
    }
	
	// Update is called once per frame
	void Update () {
        //if (IsKeyEnabled_Space)
        //{
        if (turn)
        {
            Rotate();
            // Applies forward thrust if 'w' is held down
            if (Input.GetKey(KeyCode.W) && movementLimit > 0.0f)
            {
                ApplyMovement();
            }
            // Applies friction if the player lets go of 'w'
            else
            {
                if (rb2d.velocity.magnitude > 0.1f)
                {
                    ApplyFriction();
                }
                else rb2d.velocity = Vector2.zero;
            }
            //Move();

            //float moveHorizontal = Input.GetAxis("Horizontal");
            //float moveVertical = Input.GetAxis("Vertical");
            //float rotateHorizontal = Input.GetAxis("MouseX");
            // float rotateVertical = Input.GetAxis("MouseY");
            //direction = new Vector2 (rotateHorizontal,rotateVertical);
            //Rotate();
            //transform.Translate(0, moveVertical * speed * Time.deltaTime, 0);
            //Vector2 movement = new Vector2(moveHorizontal,moveVertical);
            // rb2d.AddForce(movement*speed*Time.deltaTime);

            //var r2d = GetComponent("Rigidbody2D");
            if (Input.GetKeyDown(KeyCode.Space))
            {
                //IsKeyEnabled_Space = false;
                turn = false;
                bulletObject = Instantiate(bullet, transform.position + (Vector3)direction, Quaternion.identity);
                bulletObject.GetComponent<BulletScript>().player = gameObject;
                movementLimit = 0.0f;
                rb2d.velocity = Vector2.zero;
                rb2d.angularVelocity = 0.0f;
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

        else
        {
            rb2d.velocity = Vector2.zero;
            rb2d.angularVelocity = 0.0f;
        }
    }

    // Calculates the new velocity and applies it to the position
    /*void Move()
    {
        velocity += acceleration;
        if(velocity.magnitude > maxVelocity)
        {
            velocity.Normalize();
            velocity *= maxVelocity;
        }
        rb2d.AddForce(velocity);
        acceleration = Vector2.zero;
        
        /*position += velocity * Time.deltaTime;
        transform.position = position;

        
        position = transform.position;
    }*/

    // Applies forward thrust force
    void ApplyMovement()
    {
        Vector2 movementForce = direction;
        movementForce *= maxForce;
        //ApplyForce(movementForce);
        rb2d.AddForce(movementForce);

        movementLimit -= Time.deltaTime; //Mathf.Abs(movementForce.magnitude);
    }

    // Applies friction force
    void ApplyFriction()
    {
        Vector2 frictionForce = -rb2d.velocity * friction;
        rb2d.AddForce(frictionForce);
        //ApplyForce(frictionForce);
    }

    // Rotates the player according to mouse location
    void Rotate()
    {
        Vector2 nextDirection = (Vector2)mainCamera.ScreenToWorldPoint((Vector2)Input.mousePosition) - rb2d.position;
        float angle = Vector2.Dot(direction, nextDirection) / (direction.magnitude * nextDirection.magnitude);
        if(nextDirection.magnitude > 0.5f)
        {
            rb2d.MoveRotation(angle);
            direction = nextDirection;
            direction.Normalize();
            transform.up = (Vector3)direction;
        }
        else
        {
            direction = (Vector2)transform.up;
        }
    }

    // Applies force to acceleration
    void ApplyForce(Vector2 force)
    {
        acceleration += force / mass;
    }

    public void Damage(int damageCount)
    {
        hp -= damageCount;

        if (hp <= 0)
        {
            // Dead!
            Destroy(gameObject);
        }
    }
}
