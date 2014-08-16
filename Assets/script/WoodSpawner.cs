using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class WoodSpawner : MonoBehaviour {
    //private List<GameObject> woodList;
    //public List<Vector3> woodPosList;
    public GameObject woodprefab;
    public GameObject reinprefab;
    public GameObject bgprefab;
    public int max_wood_cnt;
    public int dy;

    private float x_min_bound;
    private float x_max_bound;

    GameObject newParent;

    private Vector3 last_wood_pos;

    void Awake()
    {
        //woodList = new List<GameObject>();
        //woodPosList = new List<Vector3>();
        x_min_bound = bgprefab.renderer.bounds.min.x + (woodprefab.renderer.bounds.max.x - woodprefab.renderer.bounds.min.x);
        x_max_bound = bgprefab.renderer.bounds.max.x - (woodprefab.renderer.bounds.max.x - woodprefab.renderer.bounds.min.x);
    }

	// Use this for initialization
	void Start () {
        int sum_y = dy;
        
        GameObject newParent = GameObject.Find("2-Midground");
        for (int i = 0; i < max_wood_cnt; i++)
        {
            Vector3 newPos = new Vector3(Random.Range(x_min_bound, x_max_bound), Random.Range(sum_y - dy, sum_y), 0);
            GameObject wood;
            if(Random.Range(0,2)%2 == 1)
                wood = Instantiate(woodprefab, newPos, Quaternion.identity) as GameObject;
            else
                wood = Instantiate(reinprefab, newPos, Quaternion.identity) as GameObject;
            wood.layer = LayerMask.NameToLayer("ground");
            sum_y += dy;
            //woodPosList.Add(newPos);
            wood.transform.parent = newParent.transform;
            last_wood_pos = newPos;
        }
        
	}

    // Update is called once per frame
    void Update()
    {
        GameObject player = GameObject.Find("Player");
        GameObject newParent = GameObject.Find("2-Midground");
        if (player.transform.position.y + 20 > last_wood_pos.y)
        {
            float last_y = last_wood_pos.y;
            Vector3 newPos = new Vector3(Random.Range(x_min_bound, x_max_bound), Random.Range(last_y+5, last_y + dy +5), 0);
            last_wood_pos = newPos;
            GameObject wood;
            if (Random.Range(0, 2) % 2 == 1)
                wood = Instantiate(woodprefab, newPos, Quaternion.identity) as GameObject;
            else
                wood = Instantiate(reinprefab, newPos, Quaternion.identity) as GameObject;
            wood.layer = LayerMask.NameToLayer("ground");
            wood.transform.parent = newParent.transform;
            //woodList.Add(wood);
            //woodList[i].transform.position = newPos;
        }
        
	}
}

