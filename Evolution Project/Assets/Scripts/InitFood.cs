using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitFood : MonoBehaviour
{

    public int NumFood;
    public float MaxRadius;

    public GameObject Prefab;

    // Start is called before the first frame update
    public void Init()
    {
        for (int i = 0; i < NumFood; i++)
        {

            float Radius = Random.Range(0, MaxRadius);

            float angle = 2 * Mathf.PI * i / NumFood;
            float x = Radius * Mathf.Cos(angle);
            float y = Radius * Mathf.Sin(angle);

            GameObject newCreature = Instantiate(Prefab);
            newCreature.transform.position = new Vector3(x, y, 1);
        }
    }

}
