using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodController
{

    private int NumFood;
    private float MaxRadius;

    private GameObject Prefab;

    public FoodController(GameObject Prefab, int NumFood, float MaxRadius)
    {
        this.Prefab = Prefab;
        this.NumFood = NumFood;
        this.MaxRadius = MaxRadius;
    }

    // Start is called before the first frame update
    public void SpawnFood()
    {
        for (int i = 0; i < NumFood; i++)
        {

            float Radius = Random.Range(0, MaxRadius);

            float angle = 2 * Mathf.PI * i / NumFood;
            float x = Radius * Mathf.Cos(angle);
            float y = Radius * Mathf.Sin(angle);

            GameObject newCreature = Object.Instantiate(Prefab);
            newCreature.transform.position = new Vector3(x, y, 1);
        }
    }

    public void DestroyFood()
    {

    }

}
