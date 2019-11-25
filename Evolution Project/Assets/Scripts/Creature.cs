using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature : MonoBehaviour
{

    [Header("Statistics")]
    public float speed;
    public float turnSpeed;

    private Rigidbody2D myRigidbody2D;

    private float theta;

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
        theta = Random.Range(0, 2 * Mathf.PI);
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        ClampVelocity();


    }

    #region MovementAPI

    private void Move()
    {
        theta += Random.Range(-turnSpeed, turnSpeed);
        float x = Mathf.Cos(theta) * speed;
        float y = Mathf.Sin(theta) * speed;
        myRigidbody2D.AddForce(new Vector2(x, y).normalized);
    }

    private void ClampVelocity()
    {
        float x = Mathf.Clamp(myRigidbody2D.velocity.x, -speed, speed);
        float y = Mathf.Clamp(myRigidbody2D.velocity.y, -speed, speed);

        myRigidbody2D.velocity = new Vector2(x, y);
    }

    #endregion
}
