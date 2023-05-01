using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    // Start is called before the first frame update
    public float maxSpeed = 2.0f;
    private float timeToLive = 5.0f;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 currPos = transform.position;

        Vector3 velocity = new Vector3(0, maxSpeed*Time.deltaTime, 0);

        currPos += transform.rotation * velocity;

        transform.position = currPos;

        timeToLive -= Time.deltaTime;

        if (timeToLive <= 0.0f){
            Destroy(gameObject); // disappear from hierarchy
        }
    }
    void OnTriggerEnter2D(Collider2D col){
        Destroy(gameObject);
    }
}
