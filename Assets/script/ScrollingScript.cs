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
                }
            }
        }
    }
}