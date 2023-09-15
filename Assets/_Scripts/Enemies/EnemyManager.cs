using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] GameObject enemiesContainer;
    [SerializeField] public List<GameObject> enemies;

    // Start is called before the first frame update
    void Start()
    {
        //for (int i = 0; i < enemiesContainer.transform.childCount; i++)
        //{
        //    enemies.Add(enemiesContainer.transform.GetChild(i).gameObject);
        //}
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void EnemyPatrol()
    {

    }
}
