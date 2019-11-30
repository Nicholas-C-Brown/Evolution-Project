using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature : MonoBehaviour
{

    //Movement
    public float Speed {
        get { return speed; }
        set {
            if (value > 0){
                speed = value;
                turnSpeed = speed / 5;
            }
        }
    }
    private float speed;
    private float turnSpeed;
    private Rigidbody2D myRigidbody2D;
    private float theta;

    //Food
    public float FoodCount { get; private set; }

    //State
    public State CurrentState { get; set; }

    public void Init()
    {
        //Movement
        myRigidbody2D = GetComponent<Rigidbody2D>();
        theta = Random.Range(0, 2 * Mathf.PI);

        //Food
        FoodCount = 0;

        //State
        CurrentState = State.ALIVE;
    }

    // Update is called once per frame
    void Update()
    {
        AI();
    }

    #region AI

    private void AI()
    {
        if (FoodCount < 2) Move();
        else Stop();
    }

    #region Movement

    private void Move()
    {
        theta += Random.Range(-turnSpeed, turnSpeed);
        float x = Mathf.Cos(theta) * speed;
        float y = Mathf.Sin(theta) * speed;

        myRigidbody2D.AddForce(new Vector2(x, y));

        ClampVelocity();
    }

    private void Stop()
    {
        myRigidbody2D.velocity = new Vector2(0, 0);
    }

    private void ClampVelocity()
    {
        float x = Mathf.Clamp(myRigidbody2D.velocity.x, -speed, speed);
        float y = Mathf.Clamp(myRigidbody2D.velocity.y, -speed, speed);

        myRigidbody2D.velocity = new Vector2(x, y);
    }

    #endregion Movement

    #region CreatureController Functions

    public void UpdateState()
    {
        if (FoodCount == 0) CurrentState = State.DEAD;
        else if (FoodCount == 1) CurrentState = State.ALIVE;
        else if (FoodCount == 2) CurrentState = State.REPRODUCE;
        else throw new UnityException("Creature food count is out of bounds");
    }

    public void ResetFoodCount()
    {
        FoodCount = 0;
    }

    #endregion CreatureController Functions

    #region FoodAPI

    public void GatherFood(GameObject food)
    {
        if (FoodCount < 2)
        {
            FoodCount++;
            Destroy(food);
        }
    }

    #endregion FoodAPI

    #endregion AI

}
