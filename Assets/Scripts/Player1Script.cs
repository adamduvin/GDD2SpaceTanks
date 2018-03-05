using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class Player1Script : MonoBehaviour {
    public GameObject bullet;
    public GameObject player2;
    [SerializeField]
    private Text turn;
    public bool myturn;

    // Use this for initialization
    void Start () {
        myturn = true;
    }
	
	// Update is called once per frame
	void Update () {
        var r2d = GetComponent("Rigidbody2D");
        if (myturn == true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                var target = GameObject.Find("TargetBox1");
                Instantiate(bullet, transform.position, transform.rotation);
                turn.text = "Turn: Player 2";
                myturn = false;
                player2.GetComponent<Player2Script>().myturn = !false;

            }
        }
    }
}
