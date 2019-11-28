using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitCreatures : MonoBehaviour
{

    public int NumCreatures;
    public float Radius;

    public GameObject Prefab;

    public void Init()
    {
        for (int i = 0; i < NumCreatures; i++)
        {
            float angle = 2 * Mathf.PI * i / NumCreatures;
            float x = Radius * Mathf.Cos(angle);
            float y = Radius * Mathf.Sin(angle);

            GameObject newCreature = Instantiate(Prefab);
            newCreature.transform.position = new Vector3(x, y, 1);
        }
    }

    
}
