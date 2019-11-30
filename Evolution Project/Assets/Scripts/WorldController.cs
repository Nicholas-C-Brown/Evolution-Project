using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldController : MonoBehaviour
{
    [Header("Unity Settings")]
    [Range(1.0f, 10.0f)]
    public float TimeScale;
    private float fixedDeltaTime;

    [Header("World Settings")]
    public World world;

    public Text RoundText;
    public int RoundCount;
    private int currentRound;

    public Text TimeText;
    public int RoundTime;
    private float currentTime;

    [Header("Creature Settings")]
    public GameObject CreaturePrefab;
    public int NumCreatures;
    public float CreatureSpawnRadius;
    public float Speed;

    public Text CreatureCountText;
    public Text CreatureBirthText;
    public Text CreatureDeathText;

    private int creatureTotal;

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

        creatureController = new CreatureController(CreaturePrefab);
        foodController = new FoodController(FoodPrefab);

        world.Init();
        creatureController.InitCreatures(NumCreatures, Speed);

    }

    private void Awake()
    {
        fixedDeltaTime = Time.fixedDeltaTime;
    }

    // Update is called once per frame
    private void Update()
    {

        if (TimeScale > 0 && TimeScale <= 10)
        {
            Time.timeScale = TimeScale;
            Time.fixedDeltaTime = fixedDeltaTime * Time.timeScale;
        }


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
                creatureTotal += creatureController.GetCreatureCount();
            }

            DrawToCanvas();

        }
        else
        {
            Debug.Log("End of Simulation");
            Debug.Log("Average # of Creatures: " + (creatureTotal / RoundCount).ToString());
        }
    }

    #region World Generation Methods

    void Spawn()
    {
        creatureController.SpawnCreatures(CreatureSpawnRadius);
        foodController.SpawnFood(NumFood, FoodSpawnRadius);
    }

    void ClearWorld()
    {
        creatureController.UpdateCreatures();
        foodController.DestroyFood();
    }

    #endregion


    #region Canvas

        void DrawToCanvas()
    {
        RoundText.text = "Round: " + currentRound.ToString() + " / " + RoundCount.ToString();
        TimeText.text = "Round Time: " + Math.Round(currentTime, 2).ToString();
        CreatureCountText.text = "Creatures: " + creatureController.GetCreatureCount().ToString() + " (Net: " + (creatureController.BirthCount - creatureController.DeathCount).ToString() + ")";
        CreatureBirthText.text = "Births: " + creatureController.BirthCount.ToString();
        CreatureDeathText.text = "Deaths: " + creatureController.DeathCount.ToString();
    }

    #endregion

}
