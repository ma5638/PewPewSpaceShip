using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidScript : MonoBehaviour
{
    // Start is called before the first frame update
    public int health = 1;
    private float timeToLive = 5.0f;
    public float rotationSpeed = 50.0f;

    // Update is called once per frame
    void Update()
    {
        
        // transform.position += (Vector3)velocity * Time.deltaTime;
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
        timeToLive -= Time.deltaTime;
        if (health <= 0 || timeToLive <= 0.0f){
            Destroy(gameObject); // disappear from hierarchy
        }
        
    }
    void OnTriggerEnter2D(Collider2D col){
        string colTag = col.gameObject.tag;
        // if (colTag != "player"){
        health--;
        // }

    }
}
