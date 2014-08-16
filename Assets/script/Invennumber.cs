using UnityEngine;
using System.Collections;

public class Invennumber : MonoBehaviour {

    //for our GUIText object and our 
    GameObject text;

    void Start() {
        text = GameObject.Find("num");
    }

    void Update()
    {
        TextMesh text_ = text.GetComponent<TextMesh>();
        text_.text = "x" + SpriteControl.num_sheild;
    }
}
