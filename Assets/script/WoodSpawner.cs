using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class WoodSpawner : MonoBehaviour
{
    //private List<GameObject> woodList;
    //public List<Vector3> woodPosList;
    public GameObject woodprefab;
    public GameObject reinprefab;
    public GameObject bgprefab;
    public GameObject crushPrefab;
    public GameObject jumpupPrefab;
    public GameObject wingPrefab;
    public GameObject sheildPrefab;
    public int max_wood_cnt;
    public int dy;
    
    public int propertyCrush = 100;
    public int propertySheild = 200;
    public int propertyJumpup = 100;
    public int propertyWing = 50;

    private float x_min_bound;
    private float x_max_bound;

    GameObject newParent;

    private Vector3 last_wood_pos;

    void Awake()
    {
        x_min_bound = bgprefab.renderer.bounds.min.x + (woodprefab.renderer.bounds.max.x - woodprefab.renderer.bounds.min.x);
        x_max_bound = bgprefab.renderer.bounds.max.x - (woodprefab.renderer.bounds.max.x - woodprefab.renderer.bounds.min.x);
    }

    // Use this for initialization
    void Start()
    {
        int sum_y = dy;

        GameObject newParent = GameObject.Find("2-Midground");
        for (int i = 0; i < max_wood_cnt; i++)
        {
            Vector3 newPos = new Vector3(Random.Range(x_min_bound, x_max_bound), Random.Range(3+sum_y - dy, 3+sum_y), 0);
            GameObject wood;
            if (Random.Range(0, 2) % 2 == 1)
                wood = Instantiate(woodprefab, newPos, Quaternion.identity) as GameObject;
            else
                wood = Instantiate(reinprefab, newPos, Quaternion.identity) as GameObject;
            wood.layer = LayerMask.NameToLayer("ground");
            wood.tag = "tiles";
            sum_y += dy;
            wood.transform.parent = newParent.transform;
            last_wood_pos = newPos;

            Vector3 AddPos = new Vector3(Random.Range(-1f, 1f), 0.7f, 0);
            GameObject item;
            int su = Random.Range(0, 1000);
            if (0 <= su && su < propertyCrush) {
                item = Instantiate(crushPrefab, newPos + AddPos, Quaternion.identity) as GameObject;
                item.tag = "Crush";
            } else
            if (propertyCrush <= su && su < propertyCrush + propertySheild) {
                item = Instantiate(sheildPrefab, newPos + AddPos, Quaternion.identity) as GameObject;
                item.tag = "Sheild";
            } else
            if (propertyCrush + propertySheild <= su && su < propertyCrush + propertySheild + propertyJumpup) {
                item = Instantiate(jumpupPrefab, newPos + AddPos, Quaternion.identity) as GameObject;
                item.tag = "Jump_up";
            } else
            if (propertyCrush + propertySheild + propertyJumpup <= su && su < propertyCrush + propertySheild + propertyJumpup + propertyWing) {
                item = Instantiate(wingPrefab, newPos + AddPos, Quaternion.identity) as GameObject;
                item.tag = "Wing";
            } else{
                item = null;
            }
            if (item != null) {
                item.layer = LayerMask.NameToLayer("items");
                item.transform.parent = newParent.transform;
            }
                
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
            Vector3 newPos = new Vector3(Random.Range(x_min_bound, x_max_bound), Random.Range(last_y + 5, last_y + dy + 5), 0);
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