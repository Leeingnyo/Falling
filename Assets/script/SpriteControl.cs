using UnityEngine;
using System.Collections;

public class SpriteControl : MonoBehaviour {
	Animator anim;
	// Use this for initialization
	void Awake () {
		anim = GetComponentInChildren<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
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
		}else{
			anim.SetBool("moving",false);
		}
	}
}
