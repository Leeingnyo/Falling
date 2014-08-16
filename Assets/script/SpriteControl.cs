using UnityEngine;
using System.Collections;

public class SpriteControl : MonoBehaviour {

	Animator anim;
    public int speed = 2;
    public float jumpForce = 100.0f;
    bool jump = false;

	// Use this for initialization
	void Awake () {
		anim = GetComponentInChildren<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 moveDir = Vector3.zero;

		int inputHor;
        if (Input.GetKey(KeyCode.LeftArrow))
            inputHor = -1;
        else if (Input.GetKey(KeyCode.RightArrow))
            inputHor = 1;
        else if (Input.GetKey(KeyCode.UpArrow))
            inputHor = 2;
        else if (Input.GetKey(KeyCode.DownArrow))
            inputHor = -2;
        else
            inputHor = 0;

		if(inputHor!=0){
			anim.SetInteger("dirc",inputHor/Mathf.Abs(inputHor));
			anim.SetBool("moving",true);
            if (inputHor == -1)
                moveDir += Vector3.left;
            else if (inputHor == 1)
                moveDir += Vector3.right;
            else if (inputHor == 2)
                moveDir += Vector3.up;
            else if (inputHor == -2)
                moveDir += Vector3.down;
		}else{
			anim.SetBool("moving",false);
		}

        transform.position += (moveDir * Time.deltaTime * speed);
	}


}
