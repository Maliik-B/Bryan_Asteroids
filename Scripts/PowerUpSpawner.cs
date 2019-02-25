using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour {

    // Use this for initialization
    public GameObject powerUp;
    public GameObject shipHolderGO;
    private GameObject power;
    private Vector3 direction;
    private Vector3 acceleration;
    private Vector3 velocity;
    private float accelerationRate;
    private float maxSpeed;
    public GameObject asteroidHolderGO;
    private Vector3 position;
    private SpriteRenderer shipSprite;
    private SpriteRenderer powerSprite;
    private int chanceOfSpawning;
    private float timer;
    private bool isThere;
    float leftConstraint;
    float rightConstraint;
    float bottomConstraint;
    float topContraint;
    float distanceZ;
    Camera cam;
    void Start () {
        timer = Time.realtimeSinceStartup;
        isThere = false;
        power = null;
        cam = Camera.main;
        leftConstraint = Screen.width;
        rightConstraint = Screen.width;
        topContraint = Screen.height;
        bottomConstraint = Screen.height;
        accelerationRate = .001f;
        maxSpeed = .1f;
        distanceZ = Mathf.Abs(cam.transform.position.z + transform.position.z);
        leftConstraint = cam.ScreenToWorldPoint(new Vector3(0f, 0f, distanceZ)).x;
        rightConstraint = cam.ScreenToWorldPoint(new Vector3(Screen.width, 0f, distanceZ)).x;
        bottomConstraint = cam.ScreenToWorldPoint(new Vector3(0f, 0f, distanceZ)).y;
        topContraint = cam.ScreenToWorldPoint(new Vector3(0f, Screen.height, distanceZ)).y;
    }
	
	// Update is called once per frame
	void Update () {
		if(timer + 5 >= Time.realtimeSinceStartup)
        {
            chanceOfSpawning = Random.Range(1, 11);
            if(chanceOfSpawning > 1 && isThere == false)
            {
                chanceOfSpawning = Random.Range(1, 4);
                power = Instantiate(powerUp, asteroidHolderGO.GetComponent<AsteroidMovement>().AsteroidPlacement(chanceOfSpawning), Quaternion.identity);
                direction = new Vector3((float)Random.Range(-180f, 180f), (float)Random.Range(-180f, 180f), 0);
                
            }
        }
        if(power != null)
        {
            acceleration = direction * accelerationRate;
            velocity += acceleration;
            velocity = Vector3.ClampMagnitude(velocity, maxSpeed);
            position += velocity;
            transform.position = position;
        }
        if(power != null)
        {
            shipSprite = shipHolderGO.GetComponent<SpriteRenderer>();
            powerSprite = power.GetComponent<SpriteRenderer>();
            float centersDistance = Mathf.Pow(powerSprite.bounds.center.x - shipSprite.bounds.center.x, 2) + Mathf.Pow(powerSprite.bounds.center.y - shipSprite.bounds.center.y, 2);
            Vector3 powerSpriteRadius = powerSprite.bounds.max - powerSprite.bounds.center;
            Vector3 shipSpriteRadius = shipSprite.bounds.max - shipSprite.bounds.center;
            float radiiDistance = Mathf.Pow(powerSpriteRadius.magnitude + shipSpriteRadius.magnitude, 2);
            if (centersDistance < radiiDistance)
            {
                Destroy(power);
                isThere = false;
                timer = Time.realtimeSinceStartup;
            }
        }
        if (power.transform.position.x < leftConstraint - 2)
        {
            position = new Vector3(rightConstraint - 1, power.transform.position.y, power.transform.position.z);
            transform.position = position;
        }
        if (power.transform.position.x > rightConstraint + 2)
        {
            position = new Vector3(leftConstraint + 1, power.transform.position.y, power.transform.position.z);
            transform.position = position;
        }
        if (power.transform.position.y < bottomConstraint - 2)
        {
            position = new Vector3(power.transform.position.x, topContraint - 1, power.transform.position.z);
            transform.position = position;
        }
        if (power.transform.position.y > topContraint + 2)
        {
            position = new Vector3(power.transform.position.x, bottomConstraint + 1, power.transform.position.z);
            transform.position = position;
        }
    }
    
}
