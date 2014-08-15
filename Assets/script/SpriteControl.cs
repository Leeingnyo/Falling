using UnityEngine;
using System.Collections;

public class SpriteControl : MonoBehaviour {

	Animator anim;
    int speed;

	// Use this for initialization
	void Awake () {
		anim = GetComponentInChildren<Animator>();
        speed = 1;
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 moveDir = Vector3.zero;

		int inputHor;
		if(Input.GetKey(KeyCode.LeftArrow))
			inputHor = -1;
		else if(Input.GetKey(KeyCode.RightArrow))
			inputHor = 1;
		else
			inputHor = 0;

		if(inputHor!=0){
			anim.SetInteger("dirc",inputHor/Mathf.Abs(inputHor));
			anim.SetBool("moving",true);
            if (inputHor == -1)
                moveDir += Vector3.left;
            else if (inputHor == 1)
                moveDir += Vector3.right;
		}else{
			anim.SetBool("moving",false);
		}

        transform.position += (moveDir * Time.deltaTime * speed);
	}
}
