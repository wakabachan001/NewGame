using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyParent : MonoBehaviour
{
    public List<GameObject> EnemyArray = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < EnemyArray.Count; i++) 
        {
            if (EnemyArray[i] != null) 
            {
                EnemyArray[i].SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
