using UnityEngine;
using System.Collections;
public class DeadSceneSelect:MonoBehaviour{
	public GameObject[] deathScenes;
	private GameObject sprite;
	void Awake(){
		iTween.CameraFadeAdd();
		iTween.CameraFadeFrom(1.0f,2.0f);
		sprite = transform.GetChild(0).gameObject;
	}
	void kill(int num){
		sprite.SetActive(false);
		deathScenes[num].SetActive(true);
		Invoke ("endScene",5.0f);
	}
	void endScene(){
		iTween.CameraFadeTo(iTween.Hash("amount",1.0f,"time",3.0f,"oncomplete","nextLevel","oncompletetarget",gameObject));
	}
	void nextLevel(){
		Application.LoadLevel(2);
	}
}

