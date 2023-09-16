using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatBackground : MonoBehaviour
{
    [Range(-1f, 1f)]
    public float scroll_speed = 0.5f;
    private float scroll_offset;
    private Material mat;

    void Start()
    {
        // initialize material
        mat = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        // generate scroll intervals
        scroll_offset += (Time.deltaTime * scroll_speed) / 10f;
        // scroll background
        mat.SetTextureOffset("_MainTex", new Vector2(0, scroll_offset));
    }
}
