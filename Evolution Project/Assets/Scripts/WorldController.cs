using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldController : MonoBehaviour
{

    public float TimeScale;

    public Text TimeText;
    public Text RoundText;
    public int RoundCount;
    private int currentRound;
    public int RoundTime;
    private float currentTime;

    public World world;
    private InitCreatures initCreatures;
    private InitFood initFood;

    private Creature[] creatures;
    private Food[] foods;
    private bool waiting;

    private void Start()
    {

        currentTime = 0;
        currentRound = 1;

        initCreatures = transform.GetComponent<InitCreatures>();
        initFood = transform.GetComponent<InitFood>();

    }

    // Update is called once per frame
    private void Update()
    {

        Time.timeScale = TimeScale;

        if (currentRound <= RoundCount)
        {

            if (currentTime == 0)
            {
                Init();
            }

            currentTime += Time.deltaTime;

            if ((currentTime > RoundTime) && !waiting)
            {
                UpdateArrays();
                ClearWorld();
                currentTime = 0;
                currentRound++;
            }

            DrawToCanvas();

        }
        else
        {
            Debug.Log("Ending Simulation");
            Application.Quit();
        }
    }

    #region Update Methods

    void UpdateArrays()
    {
        creatures = FindObjectsOfType<Creature>();
        foods = FindObjectsOfType<Food>();
    }


    #endregion

    #region World Generation Methods

    void Init()
    {
        world.Init();
        initCreatures.Init();
        initFood.Init();
    }

    void ClearWorld()
    {
        foreach (Food f in foods)
        {
            Destroy(f.gameObject);
        }

        foreach (Creature c in creatures)
        {
            Destroy(c.gameObject);
        }
    }

    #endregion

    #region Canvas

    void DrawToCanvas()
    {
        RoundText.text = "Round: " + currentRound.ToString() + " / " + RoundCount.ToString();
        TimeText.text = "Round Time: " + Math.Round(currentTime, 2).ToString();
    }

    #endregion

}
