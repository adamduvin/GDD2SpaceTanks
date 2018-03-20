using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {

    public int hp = 3;
    public List<GameObject> players;
    public GameObject currentPlayer;
    public GameObject currentenemy;
    public int playerIndex;
    private bool isPlayer1 = true;
    private bool isPlayer2 = false;
    private bool didItHit = false;


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
            if (isPlayer1 == true || currentenemy.tag == "Player2")
            {
                if (shot.transform.position == currentenemy.transform.position)
                {
                    Damage(shot.damage);

                }

                playerIndex++;
                playerIndex %= players.Count;
                currentPlayer = players[playerIndex];
                didItHit = true;


                // Destroy the shot
                Destroy(shot.gameObject); // Remember to always target the game object, otherwise you will just remove the script
                currentenemy = players[0];
                isPlayer1 = false;
                isPlayer2 = true;
            }

            if (isPlayer2 == true || currentenemy.tag == "Player1")
            {
                Damage(shot.damage);

                playerIndex++;
                playerIndex %= players.Count;
                currentPlayer = players[playerIndex];
                didItHit = true;


                // Destroy the shot
                Destroy(shot.gameObject); // Remember to always target the game object, otherwise you will just remove the script
                currentenemy = players[1];
                isPlayer2 = false;
                isPlayer1 = true;
            }

            if (didItHit == false)
            {

                playerIndex++;
                playerIndex %= players.Count;
                currentPlayer = players[playerIndex];
                if (isPlayer1 == false)
                {
                    currentenemy = players[0];
                }

                if (isPlayer2 == false)
                {
                    currentenemy = players[1];
                }


            }
            didItHit = false;
        }
    }

    // Use this for initialization
    void Start()
    {
        playerIndex = 0;
        currentPlayer = players[playerIndex];
        currentenemy = players[1];
    }

    // Update is called once per frame
    void Update()
    {

    }
}
