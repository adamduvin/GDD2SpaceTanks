using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPlayer2Script : MonoBehaviour {

    private int hp = 3;
    private bool isPlayer2 = true;

    public void Damage(int damageCount)
    {
        hp -= damageCount;

        if (hp <= 0)
        {
            // Dead!
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D otherCollider)
    {
        // Is this a shot?
        BulletScript shot = otherCollider.gameObject.GetComponent<BulletScript>();
        if (shot != null)
        {
            // Avoid friendly fire
            if (shot.isEnemyPlayerTwoShot != isPlayer2)
            {
                Damage(shot.damage);

                // Destroy the shot
                Destroy(shot.gameObject); // Remember to always target the game object, otherwise you will just remove the script
            }
        }
    }


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
