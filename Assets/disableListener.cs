using UnityEngine;
using System.Collections;

public class disableListener : MonoBehaviour {
	public int destroyEffect;
	// Use this for initialization
	void OnDisable () {
		ParticleManager.manager.setEffect(destroyEffect,transform.position);
	}
	
}
