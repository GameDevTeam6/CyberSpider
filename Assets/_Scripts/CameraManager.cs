using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] Camera cam;
    [SerializeField] GameObject player;
    private Vector3 relativePosition;
    private float halfScreenHeight;
    private float halfScreenWidth;
    private float fps;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        halfScreenHeight = cam.orthographicSize;
        halfScreenWidth = halfScreenHeight * cam.aspect;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(fps = (int)(1f / Time.unscaledDeltaTime));
        relativePosition = cam.transform.InverseTransformDirection(player.transform.position - cam.transform.position);
        //Debug.Log(new Vector2(halfScreenWidth - relativePosition.x, halfScreenHeight - relativePosition.y));
        //Debug.Log(relativePosition);
        //cam.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -10f);

        cam.transform.position = new Vector3(cam.transform.position.x, player.transform.position.y, -10f);

        //if (halfScreenHeight - relativePosition.y < 4.0f)
        //{
        //    cam.transform.Translate(0f, 0.05f, 0f);
        //}
        
        //if ((halfScreenHeight - relativePosition.y < 10.0f) && (halfScreenHeight - relativePosition.y > 6.0f))
        //{
        //    cam.transform.Translate(0f, -0.05f, 0f);
        //}
        
        //if (halfScreenWidth - relativePosition.x < 5.0f)
        //{
        //    cam.transform.Translate(0.01f, 0f, 0f);
        //}
        
        //if (halfScreenWidth - relativePosition.x > 5.0f)
        //{
        //    cam.transform.Translate(0.01f, 0.01f, 0f);
        //}
    }
}
