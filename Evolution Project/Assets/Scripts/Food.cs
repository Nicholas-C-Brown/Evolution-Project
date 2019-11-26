using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Creature CollidingCreature = collision.GetComponent<Creature>();
        CollidingCreature.GatherFood(transform.gameObject);
    }

}
