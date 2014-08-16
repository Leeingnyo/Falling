using System.Collections.Generic;
using System.Linq;
using UnityEngine;
/// <summary>
/// Parallax scrolling script that should be assigned to a layer
/// </summary>
public class ScrollingScript : MonoBehaviour
{
    public GameObject last_pref;
    private GameObject last_bg;
    private Transform child;
    private Vector3 last_bg_pos;
    private float bg_height;

    void Start()
    {
        GameObject newParent = GameObject.Find("1-Background");

        last_bg_pos = new Vector3(0, 7.2f, 10);
        last_bg = Instantiate(last_pref, last_bg_pos, Quaternion.identity) as GameObject;
        last_bg.transform.parent = newParent.transform;
        bg_height = last_bg.renderer.bounds.max.y - last_bg.renderer.bounds.min.y;
    }
    
    void Update()
    {
        GameObject newParent = GameObject.Find("1-Background");
        last_bg_pos = last_bg.transform.position;
        {
            Vector3 newPos = new Vector3(last_bg_pos.x, last_bg_pos.y + bg_height, 10);
            last_bg = Instantiate(last_pref, newPos, Quaternion.identity) as GameObject;
            last_bg.transform.parent = newParent.transform;
        }
    }
}