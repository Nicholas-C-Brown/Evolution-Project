using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldController : MonoBehaviour
{
    [Header("Unity Settings")]
    public float TimeScale;

    [Header("World Settings")]
    public World world;

    public Text TimeText;
    public Text RoundText;
    public int RoundCount;
    private int currentRound;
    public int RoundTime;
    private float currentTime;

    [Header("Creature Settings")]
    public GameObject CreaturePrefab;
    public int NumCreatures;
    public float CreatureSpawnRadius;
    public float Speed;
    public float TurnSpeed;

    private CreatureController creatureController;

    [Header("Food Settings")]
    public GameObject FoodPrefab;
    public int NumFood;
    public float FoodSpawnRadius;

    private FoodController foodController;

    private void Start()
    {

        currentTime = 0;
        currentRound = 1;

        creatureController = new CreatureController(CreaturePrefab, NumCreatures, CreatureSpawnRadius, Speed, TurnSpeed);
        foodController = new FoodController(FoodPrefab, NumFood, FoodSpawnRadius);

        world.Init();

    }

    // Update is called once per frame
    private void Update()
    {

        Time.timeScale = TimeScale;

        if (currentRound <= RoundCount)
        {

            if (currentTime == 0)
            {
                Spawn();
            }

            currentTime += Time.deltaTime;

            if ((currentTime > RoundTime))
            {
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

    #region World Generation Methods

    void Spawn()
    {
        creatureController.SpawnCreatures();
        foodController.SpawnFood();
    }

    void ClearWorld()
    {
        creatureController.DestroyCreatures();
        foodController.DestroyFood();
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
