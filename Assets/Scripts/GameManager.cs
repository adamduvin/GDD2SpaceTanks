﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public List<GameObject> players;
    public GameObject currentPlayer;
    public GameObject bullet;
    public int playerIndex;

	// Use this for initialization
	void Start () {
        playerIndex = 0;
        currentPlayer = players[playerIndex];
        currentPlayer.GetComponent<Player>().turn = true;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SwitchTurn()
    {
        BulletScript shot = bullet.gameObject.GetComponent<BulletScript>();
        currentPlayer.GetComponent<Player>().turn = false;
        playerIndex++;
        playerIndex %= players.Count;
        currentPlayer = players[playerIndex];
        currentPlayer.GetComponent<Player>().turn = true;
        currentPlayer.GetComponent<Player>().movementLimit = 1.0f;
        if (shot.hit == false)
        {
            shot.isEnemyPlayerOneShot = !shot.isEnemyPlayerOneShot;
            shot.isEnemyPlayerTwoShot = !shot.isEnemyPlayerTwoShot;
        }
    }
}
