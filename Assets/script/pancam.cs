using UnityEngine;
using System.Collections;

public class pancam : MonoBehaviour
{

    //for our GUIText object and our score
    public GUIElement gui;
    float playerHeight = 0;
    float playerVelo = 0;
    void Update()
    {
        playerHeight = Camera.main.transform.position.y;
        if ((int)playerHeight < 0)
        {
            gui.guiText.text = "게임 오버 ^오^";
        }
        else
        {
            playerVelo = Camera.main.velocity.y;
            Debug.Log(playerVelo);
            gui.guiText.text = "높이: " + ((int)playerHeight).ToString() + "m\n속도: " + ((int)playerVelo).ToString()+"m/s";
            transform.position = new Vector3(transform.position.x / 2, transform.position.y, -10);
        }
        
    }
}