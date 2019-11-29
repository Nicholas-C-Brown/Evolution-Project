using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature : MonoBehaviour
{

    private float speed;
    private float turnSpeed;

    //AI
    private Vector2 spawn;

    //Movement
    private Rigidbody2D myRigidbody2D;
    private float theta;

    //Food
    public float foodCount { get; set; }

    public State CurrentState { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        //AI
        spawn = transform.position;

        //Movement
        myRigidbody2D = GetComponent<Rigidbody2D>();
        theta = Random.Range(0, 2 * Mathf.PI);

        //Food
        foodCount = 0;

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

        if (foodCount < 1) Move();
        else if (transform.GetComponent<CircleCollider2D>().enabled ||
            myRigidbody2D.velocity.magnitude > 0) Deactivate();
        else if(Vector2.Distance(transform.position, spawn) > 0.1f) MoveToSpawn();

        ClampVelocity();
    }

    private void Move()
    {
        theta += Random.Range(-turnSpeed, turnSpeed);
        float x = Mathf.Cos(theta) * speed;
        float y = Mathf.Sin(theta) * speed;

        myRigidbody2D.AddForce(new Vector2(x, y));
    }

    private void Deactivate()
    {
        transform.GetComponent<CircleCollider2D>().enabled = false;
        myRigidbody2D.velocity = new Vector2(0, 0);

        SpriteRenderer sr = transform.GetComponent<SpriteRenderer>();
        sr.sortingOrder = -1;
        sr.color = new Color(1, 1, 1, 0.5f);
    }

    private void MoveToSpawn()
    {
        transform.position = Vector2.Lerp(transform.position, spawn, (speed * Time.deltaTime)/Vector2.Distance(transform.position, spawn));
    }

    private void ClampVelocity()
    {
        float x = Mathf.Clamp(myRigidbody2D.velocity.x, -speed, speed);
        float y = Mathf.Clamp(myRigidbody2D.velocity.y, -speed, speed);

        myRigidbody2D.velocity = new Vector2(x, y);
    }

    public void ResetCreature()
    {
        foodCount = 0;

        transform.GetComponent<CircleCollider2D>().enabled = true;

        SpriteRenderer sr = transform.GetComponent<SpriteRenderer>();
        sr.sortingOrder = 0;
        sr.color = new Color(1, 1, 1, 1);

    }

    #endregion

    #region FoodAPI

    public void GatherFood(GameObject food)
    {
        if(foodCount < 1)
        {
            foodCount = 1;
            Destroy(food);
        }
    }

    #endregion

    public float GetFoodCount()
    {
        return foodCount;
    }

    public void SetSpeed(float speed)
    {
        if (speed > 0) this.speed = speed;
    }

    public void SetTurnSpeed(float turnSpeed)
    {
        if (turnSpeed > 0) this.turnSpeed = turnSpeed;
    }

}
