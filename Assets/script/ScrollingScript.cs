using System.Collections.Generic;
using System.Linq;
using UnityEngine;
/// <summary>
/// Parallax scrolling script that should be assigned to a layer
/// </summary>
public class ScrollingScript : MonoBehaviour
{

    public bool isLooping = true;

    private List<Transform> backgroundPart;

    public GameObject woodPrefab;
    public float numWoods = 10;

    void Start()
    {
       
        if (isLooping)
        {
            backgroundPart = new List<Transform>();

            for (int i = 0; i < transform.childCount; i++)
            {
                Transform child = transform.GetChild(i);

                if (child.renderer != null)
                {
                    backgroundPart.Add(child);
                }
            }

            backgroundPart = backgroundPart.OrderBy(
              t => t.position.y
            ).ToList();
        }
        GameObject newParent = GameObject.Find("1-Background");
        Transform firstChild = backgroundPart.FirstOrDefault();
        Vector3 firstPosition = firstChild.transform.position;
        Vector3 firstSize = (firstChild.renderer.bounds.max - firstChild.renderer.bounds.min);
        Vector3 woodSize = (woodPrefab.renderer.bounds.max - woodPrefab.renderer.bounds.min);
        int dy = (int)(firstSize.y / numWoods);
        int sum_y = 0;
        for (int i = 0; i < numWoods; i++)
        {
            Vector3 newPos = new Vector3(Random.Range(firstPosition.x - firstSize.x / 2 + woodSize.x, firstPosition.x + firstSize.x / 2 - woodSize.x),
                Random.Range(firstPosition.y + woodSize.y + sum_y, firstPosition.y + firstSize.y/2 + sum_y), 0);
            GameObject wood = Instantiate(woodPrefab, newPos, Quaternion.identity) as GameObject;
            sum_y += dy;
            wood.transform.parent = newParent.transform;
        }
    }

    void Update()
    {
        if (isLooping)
        {
            Transform firstChild = backgroundPart.FirstOrDefault();
            Vector3 firstPosition = firstChild.transform.position;
            Vector3 firstSize = (firstChild.renderer.bounds.max - firstChild.renderer.bounds.min);
            if (firstChild != null)
            {
                if (firstChild.position.y + firstSize.y < Camera.main.transform.position.y)
                {
                    firstChild.position = new Vector3(firstPosition.x, firstPosition.y + firstSize.y*2, firstPosition.z);

                    backgroundPart.Remove(firstChild);
                    backgroundPart.Add(firstChild);
                    GameObject newParent = GameObject.Find("1-Background");
                    Vector3 woodSize = (woodPrefab.renderer.bounds.max - woodPrefab.renderer.bounds.min);
                    int dy = (int)(firstSize.y / numWoods);
                    int sum_y = 0;
                    for (int i = 0; i < numWoods; i++)
                    {
                        Vector3 newPos = new Vector3(Random.Range(firstPosition.x - firstSize.x / 2 + woodSize.x, firstPosition.x + firstSize.x/2 - woodSize.x),
                            Random.Range(firstPosition.y + firstSize.y / 2 + woodSize.y + 30 + sum_y, firstPosition.y + firstSize.y / 2 + 30 + sum_y), 0);
                        GameObject wood = Instantiate(woodPrefab, newPos, Quaternion.identity) as GameObject;
                        wood.transform.parent = newParent.transform;
                        sum_y += dy;
                    }
                    
                }
            }
        }
    }
}