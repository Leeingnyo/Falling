using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ParticleManager:MonoBehaviour{
	GameObject[,] EffectArray;
	static public ParticleManager manager;
	public GameObject[] ParticleList;
	public int PreparedNumber = 3;
	int indexMax;

	static private int[] _index;
	List<GameObject> setIdleList = new List<GameObject>();

	void Start() {
		manager = gameObject.GetComponent<ParticleManager>();
		indexMax = PreparedNumber;
		EffectArray = new GameObject[ParticleList.Length,PreparedNumber];
		_index = new int[ParticleList.Length];

		for(int j=0;j<ParticleList.Length;j++){
			if(ParticleList[j] != null)
			for(int i=0;i<indexMax;i++){
				EffectArray[j,i] = Instantiate(ParticleList[j]) as GameObject;
				EffectArray[j,i].SetActive(false);
			}
		}
	}
	GameObject getParticle(int ParticleNumber) {
		if(_index[ParticleNumber] == indexMax) _index[ParticleNumber] = 0;
		return EffectArray[ParticleNumber,_index[ParticleNumber]++];
	}
	public GameObject setEffect(int ParticleNumber,Vector3 pos, int dirc) {
		GameObject _effect = getParticle(ParticleNumber);
		_effect.transform.position = pos;
		_effect.transform.localScale = new Vector3(dirc,1,1);
		_effect.SetActive(true);
		Invoke("setIdle",1.0f);
		setIdleList.Add(_effect);
		return _effect;
	}
	public GameObject setBullet(int ParticleNumber,Vector3 pos, int dirc) {
		GameObject _effect = getParticle(ParticleNumber);
		_effect.transform.position = pos;
		_effect.transform.localScale = new Vector3(dirc,1,1);
		_effect.SetActive(true);
		return _effect;
	}
	void setIdle(){
		setIdleList[0].SetActive(false);
		setIdleList.RemoveAt(0);
	}
//	void OnDisable() {
//		foreach(GameObject obj in EffectArray){
//			Destroy(obj);
//		}
//		EffectArray = null;
//	}
}


