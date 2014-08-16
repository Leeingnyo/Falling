using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class WoodSpawner : MonoBehaviour {
    private List<GameObject> woodList;
    public List<Vector3> woodPosList;
    public GameObject woodprefab;
    public GameObject bgprefab;
    public int max_wood_cnt;
    public int dy;

    private float x_min_bound;
    private float x_max_bound;

    GameObject newParent;

    void Awake()
    {
        woodList = new List<GameObject>();
        woodPosList = new List<Vector3>();
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
            GameObject wood = Instantiate(woodprefab, newPos, Quaternion.identity) as GameObject;
            sum_y += dy;
            woodList.Add(wood);
            woodPosList.Add(newPos);
            wood.transform.parent = newParent.transform;
        }
	}

    // Update is called once per frame
    void Update()
    {
        GameObject player = GameObject.Find("Player");
        for (int i = 0; i < max_wood_cnt; i++)
        {
            if (player.transform.position.y - 5 > woodList[i].transform.position.y)
            {
                int index = i;
                if (index == 0)
                    index = max_wood_cnt;
                float last_y = woodList[index - 1].transform.position.y;
                Vector3 newPos = new Vector3(Random.Range(x_min_bound, x_max_bound), Random.Range(last_y+5, last_y + dy +5), 0);
                woodPosList.Add(newPos);
                woodList[i].transform.position = newPos;
            }
        }
	}
}

