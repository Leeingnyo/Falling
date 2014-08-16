using UnityEngine;
using System.Collections;

public class SpriteControl : MonoBehaviour {

	Animator anim;
    int speed = 2;
    float jumpForce = 100.0f;
    bool jump = false;

	// Use this for initialization
	void Awake () {
		anim = GetComponentInChildren<Animator>();
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

        if (Input.GetKey(KeyCode.Space))
            if (jump == false) { //grounded 도 추가
                anim.SetBool("inAir", true);
                jump = true;
            }

        transform.position += (moveDir * Time.deltaTime * speed);
	}

    void FixedUpdate() {
        if (jump) {
            rigidbody2D.AddForce(new Vector2(0f, jumpForce));
        }
        jump = false;
    }

    void OnCollisionStay2D(Collision2D coll) {
        anim.SetBool("inAir", false);
    }
}
