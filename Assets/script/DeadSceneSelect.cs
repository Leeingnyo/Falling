using UnityEngine;
using System.Collections;
public class DeadSceneSelect:MonoBehaviour{
	public GameObject[] deathScenes;
	private GameObject sprite;
	void Awake(){
		sprite = transform.GetChild(0).gameObject;
	}
	void kill(int num){
		sprite.SetActive(false);
		deathScenes[num].SetActive(true);
	}
}

