using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public List<GameObject> players;
    public GameObject currentPlayer;
    //public GameObject bullet;
    public int playerIndex;
    public GameObject[] statusBars = new GameObject[2];
    public HealthFuelBar statusBarScript;
    public GameObject fuelBarPrefab;
    public bool gameOver;

	// Use this for initialization
	void Start () {
        gameOver = false;
        playerIndex = 0;
        currentPlayer = players[playerIndex];
        currentPlayer.GetComponent<Player>().turn = true;

        for (int i = 0; i < statusBars.Length; i++)
        {
            players[i].GetComponent<Player>().statusBar = statusBars[i];
        }
        statusBarScript = currentPlayer.GetComponent<Player>().statusBar.GetComponent<HealthFuelBar>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SwitchTurn()
    {
        if (gameOver == true)
        {
            Debug.Log("Game Over");
			Application.LoadLevel ("gameOver");

        }

        else
        {  
            GameObject.Destroy(statusBarScript.fuelBar);
            statusBarScript.fuelBar = GameObject.Instantiate(fuelBarPrefab, statusBarScript.positionFuelStatic, Quaternion.identity);
            statusBarScript.fuelEndCap.SetActive(true);
            statusBarScript.newScaleFuel = statusBarScript.fuelBar.transform.localScale;
            statusBarScript.positionFuel = statusBarScript.fuelBar.transform.position;

            currentPlayer.GetComponent<Player>().turn = false;
            playerIndex++;
            playerIndex %= players.Count;
            currentPlayer = players[playerIndex];
            currentPlayer.GetComponent<Player>().turn = true;
            currentPlayer.GetComponent<Player>().movementLimit = 1.0f;
            currentPlayer.GetComponent<Player>().prevFuel = 1.0f;
            statusBarScript = currentPlayer.GetComponent<Player>().statusBar.GetComponent<HealthFuelBar>();
        }
    }
}
