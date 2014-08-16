using UnityEngine;
using System.Collections;

public class pancam : MonoBehaviour
{

    //for our GUIText object and our score
    public GUIElement gui;
    float playerScore = 0;


    void Update()
    {
        //check that player exists and then proceed. otherwise we get an error when player dies
        
        playerScore = Camera.main.transform.position.y;
        if ((int)playerScore < 0)
        {
            gui.guiText.text = "게임 오버 ^오^";
        }
        else
        {
            gui.guiText.text = "높이: " + ((int)playerScore).ToString() + "m";
            transform.position = new Vector3(transform.position.x / 2, transform.position.y, -10);
        }
        
    }
}