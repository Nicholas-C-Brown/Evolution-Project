using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodController
{

    private GameObject Prefab;

    public FoodController(GameObject Prefab)
    {
        this.Prefab = Prefab;
    }

    // Start is called before the first frame update
    public void SpawnFood(int num, float maxRadius)
    {
        for (int i = 0; i < num; i++)
        {
            float Radius = Random.Range(0, maxRadius);

            float angle = 2 * Mathf.PI * i / num;
            float x = Radius * Mathf.Cos(angle);
            float y = Radius * Mathf.Sin(angle);

            GameObject newFood = Object.Instantiate(Prefab);
            newFood.transform.position = new Vector3(x, y, 1);
        }
    }

    public void DestroyFood()
    {
        Food[] food = Object.FindObjectsOfType<Food>();
        foreach (Food f in food) Object.Destroy(f.gameObject);
    }

}
