using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemyScript : MonoBehaviour
{
    public int health = 1;
    private float timeToLive = 20.0f;
    private Transform playerTransform;


    public GameObject bulletPrefab;
    public float bulletOffset = 0.8f;
    public float fireDelay = 3.0f;
    private float coolDown = 0.5f;
    public float fireCoolDown = 1.0f;

    void Start()
    {
        // find player object (so we can look at it in the Update)
        GameObject playerObject = GameObject.FindWithTag("player");
        playerTransform = playerObject.transform;
    }

    void Update()
    {
        // transform.LookAt(playerTransform.position, Vector3.forward);
        Vector3 direction = playerTransform.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(Vector3.forward, direction);
        // Apply the rotation to this object
        transform.rotation = rotation;


        timeToLive -= Time.deltaTime;
        coolDown -= Time.deltaTime;

        if (health <= 0 || timeToLive <= 0.0f){
            Destroy(gameObject); // disappear from hierarchy
        }

        if (coolDown <= 0.0f){
            float scale = transform.localScale.y;
            coolDown = fireCoolDown;
            Debug.Log(gameObject.GetComponent<BoxCollider2D>().size.y);
            Vector3 bulletPosition = transform.position + transform.up * (scale*gameObject.GetComponent<BoxCollider2D>().size.y/2 + bulletOffset);
            GameObject bullet = Instantiate(bulletPrefab, bulletPosition, transform.rotation);
        }
    }

    void OnTriggerEnter2D(Collider2D col){
        string colTag = col.gameObject.tag;
        health--;
    }
}
