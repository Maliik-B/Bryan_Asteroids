using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class AsteroidMovement : MonoBehaviour {

    // Use this for initialization
    public GameObject asteroid1;
    public GameObject asteroid2;
    private Vector3 basevelocity;
    private int maxAsteroids;
    private int maxAsteroid1;
    private int maxAsteroid2;
    public List<GameObject> asteroids;
    public List<GameObject> smallerAsteroids;
    private List<Vector3> directions;
    private List<Vector3> smallerDirections;
    private Vector3 currentPosition;
    private Vector3 currentAsteroidDirection;
    private int score;
    public Text scorePrint;
    private Vector3 currentAsteroidPosition;
    private Vector3 currentVelocity;
    private GameObject currentAsteroid;
    Vector3 acceleration;
    private List<Vector3> velocity;
    private List<Vector3> smallerVelocities;
    float accelerationRate = 0.001f;
    float maxspeed;
    //Bounding things
    float leftConstraint;
    float rightConstraint;
    float bottomConstraint;
    float topContraint;
    float distanceZ;
    Camera cam;
    private Vector3 asteroidPosition;

    void Start()
    {
        basevelocity = new Vector3(0, 0, 0);
        asteroids = new List<GameObject>();
        smallerAsteroids = new List<GameObject>();
        smallerDirections = new List<Vector3>();
        smallerVelocities = new List<Vector3>();
        directions = new List<Vector3>();
        maxAsteroids = 5;
        maxAsteroid1 = Random.Range(1, 5);
        maxAsteroid2 = maxAsteroids - maxAsteroid1;
        maxspeed = .01f;
        velocity = new List<Vector3>();
        cam = Camera.main;
        leftConstraint = Screen.width;
        rightConstraint = Screen.width;
        topContraint = Screen.height;
        bottomConstraint = Screen.height;
        score = 0;
        scorePrint.text = "Score: " + score;
        distanceZ = Mathf.Abs(cam.transform.position.z + transform.position.z);
        leftConstraint = cam.ScreenToWorldPoint(new Vector3(0f, 0f, distanceZ)).x;
        rightConstraint = cam.ScreenToWorldPoint(new Vector3(Screen.width, 0f, distanceZ)).x;
        bottomConstraint = cam.ScreenToWorldPoint(new Vector3(0f, 0f, distanceZ)).y;
        topContraint = cam.ScreenToWorldPoint(new Vector3(0f, Screen.height, distanceZ)).y;


        for (int i =0; i<maxAsteroid1; i++)
        {
            currentAsteroidPosition = AsteroidPlacement(Random.Range(1, 5));
            currentAsteroid = Instantiate(asteroid1, currentAsteroidPosition, Quaternion.identity);
            asteroids.Add(currentAsteroid);
            currentAsteroidDirection = new Vector3((float)Random.Range(-180f, 180f), (float)Random.Range(-180f, 180f),0);
            directions.Add(currentAsteroidDirection);
            velocity.Add(new Vector3(basevelocity.x,basevelocity.y,basevelocity.z));
        }
        for (int i = 0; i < maxAsteroid2; i++)
        {
            currentAsteroidPosition = AsteroidPlacement(Random.Range(1, 5));
            currentAsteroid = Instantiate(asteroid2, currentAsteroidPosition, Quaternion.identity);
            asteroids.Add(currentAsteroid);
            currentAsteroidDirection = new Vector3((float)Random.Range(-180f, 180f), (float)Random.Range(-180f, 180f), 0);
            directions.Add(currentAsteroidDirection);
            velocity.Add(new Vector3(basevelocity.x, basevelocity.y, basevelocity.z));
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (asteroids.Count < 5)
        {
            currentAsteroidPosition = AsteroidPlacement(Random.Range(1, 5));
            currentAsteroid = Instantiate(asteroid2, currentAsteroidPosition, Quaternion.identity);
            asteroids.Add(currentAsteroid);
            currentAsteroidDirection = new Vector3((float)Random.Range(-180f, 180f), (float)Random.Range(-180f, 180f), 0);
            directions.Add(currentAsteroidDirection);
            velocity.Add(new Vector3(basevelocity.x, basevelocity.y, basevelocity.z));
        }

        if (asteroids.Count > 0)
        {
            for (int i = 0; i < asteroids.Count; i++)
            {
                acceleration = directions[i] * accelerationRate;
                velocity[i] += acceleration;
                velocity[i] = Vector3.ClampMagnitude(velocity[i], maxspeed);
                currentPosition = asteroids[i].transform.position + velocity[i];
                asteroids[i].transform.position = currentPosition;
            }
        }
        if (smallerAsteroids.Count > 0)
        {
            for (int i = 0; i < smallerAsteroids.Count; i++)
            {
                acceleration = smallerDirections[i] * accelerationRate;
                smallerVelocities[i] += acceleration;
                smallerVelocities[i] = Vector3.ClampMagnitude(smallerVelocities[i], maxspeed);
                currentPosition = smallerAsteroids[i].transform.position + smallerVelocities[i];
                smallerAsteroids[i].transform.position = currentPosition;
            }
        }
        foreach(GameObject a in asteroids)
        {
            if (a.transform.position.x < leftConstraint - 2)
            {
                asteroidPosition = new Vector3(rightConstraint - 1, a.transform.position.y, a.transform.position.z);
                a.transform.position = asteroidPosition;
            }
            if (a.transform.position.x > rightConstraint + 2)
            {
                asteroidPosition = new Vector3(leftConstraint + 1, a.transform.position.y, a.transform.position.z);
                a.transform.position = asteroidPosition;
            }
            if (a.transform.position.y < bottomConstraint - 2)
            {
                asteroidPosition = new Vector3(a.transform.position.x, topContraint - 1, a.transform.position.z);
                a.transform.position = asteroidPosition;
            }
            if (a.transform.position.y > topContraint + 2)
            {
                asteroidPosition = new Vector3(a.transform.position.x, bottomConstraint + 1, a.transform.position.z);
                a.transform.position = asteroidPosition;
            }
        }
        foreach (GameObject a in smallerAsteroids)
        {
            if (a.transform.position.x < leftConstraint - 2)
            {
                asteroidPosition = new Vector3(rightConstraint - 1, a.transform.position.y, a.transform.position.z);
                a.transform.position = asteroidPosition;
            }
            if (a.transform.position.x > rightConstraint + 2)
            {
                asteroidPosition = new Vector3(leftConstraint + 1, a.transform.position.y, a.transform.position.z);
                a.transform.position = asteroidPosition;
            }
            if (a.transform.position.y < bottomConstraint - 2)
            {
                asteroidPosition = new Vector3(a.transform.position.x, topContraint - 1, a.transform.position.z);
                a.transform.position = asteroidPosition;
            }
            if (a.transform.position.y > topContraint + 2)
            {
                asteroidPosition = new Vector3(a.transform.position.x, bottomConstraint + 1, a.transform.position.z);
                a.transform.position = asteroidPosition;
            }
        }



    }
    public List<GameObject> Asteroids
    {
        get { return asteroids; }
    }
    public List<GameObject> SmallerAsteroids
    {
        get { return smallerAsteroids; }
    }
    public void SplitAsteroids(GameObject asteroid, bool isSmall)
    {
        if (isSmall)
        {
            score += 20;
            int index = smallerAsteroids.IndexOf(asteroid);
            smallerVelocities.RemoveAt(index);
            smallerDirections.RemoveAt(index);
            SmallerAsteroids.RemoveAt(index);
            Destroy(asteroid);
        }
        else
        {
            for (int i = 0; i < 2; i++)
            {
                int a = Random.Range(1, 3);
                if(a == 1)
                {
                    currentAsteroid = Instantiate(asteroid1, new Vector3(asteroid.transform.position.x, asteroid.transform.position.y, asteroid.transform.position.z), Quaternion.identity);
                }
                else
                {
                    currentAsteroid = Instantiate(asteroid2, new Vector3(asteroid.transform.position.x, asteroid.transform.position.y, asteroid.transform.position.z), Quaternion.identity);
                }
                
                currentAsteroid.transform.localScale = new Vector3(0.25f, 0.25f);
                smallerAsteroids.Add(currentAsteroid);
                currentAsteroidDirection = new Vector3((float)Random.Range(-180f, 180f), (float)Random.Range(-180f, 180f), 0);
                smallerDirections.Add(currentAsteroidDirection);
                smallerVelocities.Add(new Vector3(basevelocity.x, basevelocity.y, basevelocity.z));
            }
            score += 50;
            int r = asteroids.IndexOf(asteroid);
            velocity.RemoveAt(r);
            directions.RemoveAt(r);
            asteroids.RemoveAt(r);
            Destroy(asteroid);
        }
        scorePrint.text = "Score: " + score;

    }
    public Vector3 AsteroidPlacement(int a)
    {
        Vector3 asteroidplace;
        if(a == 1)
        {
            asteroidplace = new Vector3(leftConstraint - 1, Random.Range(topContraint, bottomConstraint), 0);
        }
        else if(a == 2)
        {
            asteroidplace = new Vector3(rightConstraint - 1, Random.Range(topContraint, bottomConstraint), 0);
        }
        else if(a == 3)
        {
            asteroidplace = new Vector3(Random.Range(leftConstraint, rightConstraint), topContraint, 0);
        }
        else
        {
            asteroidplace = new Vector3(Random.Range(leftConstraint, rightConstraint), bottomConstraint, 0);
        }
        return asteroidplace;
    }
}
