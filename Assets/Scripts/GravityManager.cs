using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityManager : MonoBehaviour {

    public GameObject projectile;               // Player's projectile
    private Rigidbody2D rigidbodyComponent;     // Projectile's rigid body 
    private CircleCollider2D boundingCircle;    // Projectile's bounding circle
    public GameObject[] obstacles;              // List of obstacles
    public float gravConst;                     // Gravity constant
    private Vector2 velocity;                   // Projectile velocity

    // Use this for initialization
    void Start () {
        gravConst = 3.6f;      // Set gravity constant

    }
	
	// Update is called once per frame
	void Update () {
        
	}

    // Calculates the force of gravity
    Vector2 CalcGravity(float massProj, float massObst, float distance, Vector2 obstPos)
    {
        float force = (gravConst * massProj * massObst) / (distance * distance);
        Vector2 forceGrav = new Vector2((obstPos.x - projectile.transform.position.x), (obstPos.y - projectile.transform.position.y));
        forceGrav *= force;
        return forceGrav;
    }

    public Vector2 MoveProjectile()
    {
        // If projectile exists, find the velocity caused by gravity and return it as a Vector2
        if (projectile != null)
        {
            for (int i = 0; i < obstacles.Length; i++)
            {
                Vector2 projPosition = projectile.transform.position;
                Vector2 obstPosition = obstacles[i].transform.position;
                Vector2 distance = projPosition - obstPosition;

                Vector2 gravity = CalcGravity(projectile.GetComponent<BulletScript>().mass, obstacles[i].GetComponent<ObstacleProperties>().mass, distance.magnitude, obstacles[i].transform.position);
                Vector2 acceleration = gravity / projectile.GetComponent<BulletScript>().mass;
                velocity += acceleration;
            }
            return velocity;
        }
        return Vector2.zero;
    }
    
    // As a projectile is made, it calls this method to let the manager know it exists
    public void AssignProjectile(GameObject projectile)
    {
        this.projectile = projectile;
        rigidbodyComponent = projectile.GetComponent<Rigidbody2D>();
        boundingCircle = projectile.GetComponent<CircleCollider2D>();
        velocity = rigidbodyComponent.velocity;
    }

    // As a projectile destroys itself, it calls this method to let the manager know it's gone
    public void NullProjectile()
    {
        projectile = null;
    }
}