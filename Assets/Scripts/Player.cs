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

    public GameObject statusBar;
    public float prevFuel;

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
        prevFuel = movementLimit;
        


        rb2d = GetComponent<Rigidbody2D>();
        IsKeyEnabled_Space = true;
    }
	
	// Update is called once per frame
	void Update () {
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
        }

        else
        {
            rb2d.velocity = Vector2.zero;
            rb2d.angularVelocity = 0.0f;
        }
    }

    // Applies forward thrust force
    void ApplyMovement()
    {
        Vector2 movementForce = direction;
        movementForce *= maxForce;
        rb2d.AddForce(movementForce);

        movementLimit -= Time.deltaTime;
        float fuelDecrement = prevFuel - movementLimit;
        prevFuel = movementLimit;
        statusBar.GetComponent<HealthFuelBar>().Decrement("fuel", fuelDecrement);
    }

    // Applies friction force
    void ApplyFriction()
    {
        Vector2 frictionForce = -rb2d.velocity * friction;
        rb2d.AddForce(frictionForce);
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
        statusBar.GetComponent<HealthFuelBar>().Decrement("health", 0.0f);

        if (hp <= 0)
        {
            // Dead!
            Destroy(gameObject);
        }
    }
}
