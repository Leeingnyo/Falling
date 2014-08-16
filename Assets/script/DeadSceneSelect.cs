using UnityEngine;
using System.Collections;
public class DeadSceneSelect:MonoBehaviour{
	public float[] ScoreSplit;
	public GameObject[] DeadScenes;
	private GameObject sprite;
	void Awake(){
//		transform.
	}
	void kill(float score){
		for(int i = 0; i < DeadScenes.Length; i++){
			if(score < ScoreSplit[i]){

			}
	}
}

