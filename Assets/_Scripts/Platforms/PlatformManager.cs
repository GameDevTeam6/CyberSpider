using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    [SerializeField] GameObject platformContainer;

    private List<GameObject> platforms;

    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform child in platformContainer.transform)
        {
            platforms.Add(child.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
