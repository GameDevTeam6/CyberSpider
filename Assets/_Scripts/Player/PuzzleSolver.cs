using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleSolver : MonoBehaviour
{
    [SerializeField] InputManager inputManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D trig)
    {
        if (trig.gameObject.CompareTag("ProgressPuzzle"))
        {
            Debug.Log("Enter puzzle.");
            inputManager.OpenQuestionPanel(() => {
                //trig.gameObject.GetComponent<ProgressPuzzleInfo>().platform.transform.GetComponent<ProgressPlatform>().solved = true;
                trig.gameObject.GetComponent<ProgressPuzzleInfo>().UnlockPlatform();
                trig.gameObject.GetComponent<CircleCollider2D>().enabled = false;
                trig.gameObject.GetComponent<SpriteRenderer>().color = new Vector4(255, 255, 255, 150);
            });
        }
    }
}
