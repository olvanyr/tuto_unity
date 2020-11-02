using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_patrol : MonoBehaviour
{

    public float speed;
    public Transform[] waypoints;

    public SpriteRenderer graphics;


    public int damage;

    private Transform target;
    private int destPoint = 0;

    // Start is called before the first frame update
    void Start()
    {
        target = waypoints[0];
    }

    // Update is called once per frame
    void Update()
    {
        // get the direction to go, target.position will be the position of our waypoint and transform.pôsition is the self position of the ennemy
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World); // normalized is used to set the vector to 1 and so the dir is now part of a vector of length 1 (so i can multiply it by my speed)
        if (Vector3.Distance(transform.position, target.position) < 0.3f)
        {
            destPoint = (destPoint + 1) % waypoints.Length; // % mean whe get the reste of a division, so if we divide a number by my array length we take back the first point if our array index is greatert than our array
            target = waypoints[destPoint]; //target and destPoint are the same thing, exepte thzt target is the object and destPoint is only an int who is the index of the target 
            graphics.flipX = !graphics.flipX;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.transform.GetComponent<PlayerHealth>();
            playerHealth.TakeDamage(damage);
        }
    }
}
