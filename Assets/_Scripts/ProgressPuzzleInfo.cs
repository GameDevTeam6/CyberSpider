using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressPuzzleInfo : MonoBehaviour
{
    public GameObject platform;

    [SerializeField] Color lockedCol;
    [SerializeField] Color unlockedCol;

    private SpriteRenderer sprite;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        sprite.color = lockedCol;
    }

    public void UnlockPlatform()
    {
        platform.GetComponent<ProgressPlatform>().solved = true;
        sprite.color = unlockedCol;
    }

}
