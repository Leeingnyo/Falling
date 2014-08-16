using System.Collections.Generic;
using System.Linq;
using UnityEngine;
/// <summary>
/// Parallax scrolling script that should be assigned to a layer
/// </summary>
public class ScrollingScript : MonoBehaviour
{
    public GameObject last_pref;
    private GameObject bg1;
	private GameObject bg2;
    private Transform child;
    private Vector3 last_bg_pos;
    private float bg_height;

	private GameObject newParent;
	private GameObject cam;

    void Start()
    {
		cam = Camera.main.gameObject;
        newParent = GameObject.Find("1-Background");

        last_bg_pos = new Vector3(0, 7.2f, 10);
        bg1 = Instantiate(last_pref, last_bg_pos, Quaternion.identity) as GameObject;
		bg2 = Instantiate(last_pref, last_bg_pos+Vector3.up*7.2f, Quaternion.identity) as GameObject;
	
		bg1.transform.parent = newParent.transform;
		bg2.transform.parent = newParent.transform;
    }
    
    void Update()
    {
		float campos = cam.transform.position.y - 3.6f;
		bg1.transform.position = new Vector3(0,Mathf.Floor(campos/7.2f)*7.2f,10);
		bg2.transform.position = new Vector3(0,Mathf.Ceil(campos/7.2f)*7.2f,10);
    }
}