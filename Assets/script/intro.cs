using UnityEngine;
using System.Collections;

public class intro : MonoBehaviour {
	private bool ready = false;
	// Use this for initialization
	void Start () {
		iTween.CameraFadeAdd();
		iTween.CameraFadeFrom(iTween.Hash("amount",1.0f,"time",2f,"oncomplete","Ready","oncompletetarget",gameObject));
	}

	void Ready(){
		ready = true;
	}
	
	// Update is called once per frame
	void Update () {
		if(ready && Input.GetButtonDown("Jump")){
			iTween.CameraFadeTo(iTween.Hash("amount",1.0f,"time",3f,"oncomplete","go","oncompletetarget",gameObject));
		}
	}

	void go(){
		Application.LoadLevel(1);
	}
}
