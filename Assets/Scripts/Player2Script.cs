using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player2Script : MonoBehaviour {
    public GameObject bullet;
    public GameObject player1;
    [SerializeField]
    private Text turn;
    public bool myturn;

    // Use this for initialization
    void Start () {
        myturn = false;
    }

    // Update is called once per frame
    void Update() {
        var r2d = GetComponent("Rigidbody2D");
        if (myturn == true)
            if (Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                Instantiate(bullet, transform.position, Quaternion.identity);
                turn.text = "Turn: Player 1";
                myturn = false;
                player1.GetComponent<Player1Script>().myturn = true;


            }
    }

    }



