using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPlayer1Script : MonoBehaviour {
    public int hp = 3;
    private bool isPlayer1 = true;

    public void Damage(int damageCount)
    {
        hp -= damageCount;

        if (hp <= 0)
        {
            // Dead!
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        // Is this a shot?
        BulletScript shot = other.gameObject.GetComponent<BulletScript>();
        if (shot != null)
        {
            // Avoid friendly fire
            if (shot.isEnemyPlayerOneShot != isPlayer1)
            {
                shot.hit = true;
                Damage(shot.damage);
                shot.isEnemyPlayerOneShot = !shot.isEnemyPlayerOneShot;
                shot.isEnemyPlayerTwoShot = !shot.isEnemyPlayerTwoShot;
                // Destroy the shot
                Destroy(shot.gameObject); // Remember to always target the game object, otherwise you will just remove the script
            }

           
            shot.hit = false;
        }
    }


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
