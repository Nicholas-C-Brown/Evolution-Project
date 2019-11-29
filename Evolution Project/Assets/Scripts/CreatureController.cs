using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CreatureController
{
    private GameObject Prefab;
    private float Radius;

    private GameObject[] creatures;

    public CreatureController(GameObject Prefab, int NumCreatures, float Radius, float Speed, float TurnSpeed)
    {
        this.Prefab = Prefab;
        this.Radius = Radius;

        InitCreatures(NumCreatures, Speed, TurnSpeed);
    }

    private void InitCreatures(int num, float speed, float turnSpeed)
    {
        creatures = new GameObject[num];

        for (int i = 0; i<num; i++)
        {
            creatures[i] = Object.Instantiate(Prefab);
            creatures[i].GetComponent<Creature>().SetSpeed(speed);
            creatures[i].GetComponent<Creature>().SetTurnSpeed(turnSpeed);
            creatures[i].SetActive(false);
        }

    }

    public void SpawnCreatures()
    {
        int i = 0;

        foreach (GameObject c in creatures)
        {
            if (c.GetComponent<Creature>().CurrentState == State.ALIVE) { 
                float angle = 2 * Mathf.PI * i / creatures.Length;
                float x = Radius * Mathf.Cos(angle);
                float y = Radius * Mathf.Sin(angle);

                c.transform.position = new Vector3(x, y, 1);
                c.SetActive(true);
            }
            i++;
        }
    }

    public void DestroyCreatures()
    {
        foreach (GameObject c in creatures)
        {
            Creature creatureComponent = c.GetComponent<Creature>();

            if (creatureComponent.GetFoodCount() < 1) creatureComponent.CurrentState = State.DEAD;
            creatureComponent.ResetCreature();
            c.SetActive(false);
        }
    }

    
}
