using UnityEngine;
using System.Collections;

public class pancam : MonoBehaviour
{

    //for our GUIText object and our score
    public GUIElement gui;
    float playerHeight = 0;
    float playerVelo = 0;
    GameObject Player;

    void Start()
    {
        Player = GameObject.Find("Player");
    }

    void Update()
    {
        playerHeight = Camera.main.transform.position.y-3;
        if ((int)playerHeight < 0)
        {
            gui.guiText.text = "게임 오버 ^오^";
        }
        else
        {
            if(playerHeight > 0)
                playerVelo = Camera.main.velocity.y;

            gui.guiText.text = ((int)playerHeight).ToString() + "m\n" + ((int)Player.rigidbody2D.velocity.y).ToString()+"m/s";
            transform.position = new Vector3(transform.position.x / 2, transform.position.y, -10);
        }
        
    }
}