using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressPlatform : MonoBehaviour
{
    [SerializeField] Color lockedColor;
    [SerializeField] Color unlockedColor;

    public bool solved = false;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.simulated = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (solved)
        {
            UnlockPlatform();
            rb.simulated = true;
        }
        
    }

    private void UnlockPlatform()
    {
        GetComponent<SpriteRenderer>().color = unlockedColor;
    }
}
