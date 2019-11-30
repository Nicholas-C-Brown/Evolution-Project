using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CreatureController
{
    private GameObject prefab;

    private List<GameObject> creatures;

    public int BirthCount { get; private set; }
    public int DeathCount { get; private set; }

    public CreatureController(GameObject Prefab)
    {
        prefab = Prefab;
        ResetCounts();
    }

    public void InitCreatures(int num, float speed)
    {
        creatures = new List<GameObject>(num);

        for (int i = 0; i<num; i++)
        {
            CreateNewCreature(speed);
        }

    }

    public void SpawnCreatures(float Radius)
    {
        int i = 0;

        foreach (GameObject go in creatures)
        {
            if (go.GetComponent<Creature>().CurrentState == State.ALIVE) { 
                float angle = 2 * Mathf.PI * i / creatures.Count;
                float x = Radius * Mathf.Cos(angle);
                float y = Radius * Mathf.Sin(angle);

                go.transform.position = new Vector3(x, y, 1);
                go.SetActive(true);
            }
            i++;
        }
    }

    public void UpdateCreatures()
    {

        foreach (GameObject go in creatures)
        {
            Creature creatureComponent = go.GetComponent<Creature>();
            creatureComponent.UpdateState();
            creatureComponent.ResetFoodCount();
        }

        UpdateCreatureList();
    }

    private void UpdateCreatureList()
    {

        ResetCounts();
        for (int i = 0; i < creatures.Count; i++)
        {
            Creature c = creatures[i].GetComponent<Creature>();

            if (c.CurrentState == State.REPRODUCE)
            {
                CreateNewCreature(c.Speed);
                c.CurrentState = State.ALIVE;
                BirthCount++;
            }

            if (c.CurrentState == State.DEAD && c.gameObject.activeSelf)
            {
                c.gameObject.SetActive(false);
                DeathCount++;
            }
        }

        
    }

    private void CreateNewCreature(float Speed)
    {
        GameObject creature = Object.Instantiate(prefab);
        creatures.Add(creature);

        int i = creatures.IndexOf(creature);

        creatures[i].GetComponent<Creature>().Init();
        creatures[i].GetComponent<Creature>().Speed = Speed;
        creatures[i].SetActive(false);
    }

    public void ResetCounts()
    {
        BirthCount = 0;
        DeathCount = 0;
    }

    public int GetCreatureCount()
    {
        int count = 0;
        foreach (GameObject go in creatures)
        {
            if (!(go.GetComponent<Creature>().CurrentState == State.DEAD)) count++;
        }

        return count;
    }

}
