using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ShipChecking : MonoBehaviour {

    // Use this for initialization
    SpriteRenderer ship;
    SpriteRenderer currentAsteroid;
    public AudioSource shipAsteroidEffect;
    GameObject asteroidScript;
    GameObject asteroidToDestroy;
    List<GameObject> asteroidlist;
    List<GameObject> smallerAsteroidList;
    private GameObject asteroidHolder;
    private GameObject gameOverLoader;
    public Text livesPrint;
    public GameObject shipHolder;
    private int lives;
    private Vector3 originSetter;
    public int count;
    void Start()
    {
        lives = 3;
        gameOverLoader = GameObject.Find("GameOverManager");
        livesPrint.text = "Lives: " + lives.ToString();
        asteroidToDestroy = null;
        originSetter = new Vector3(0, 0, 0);
        asteroidHolder = GameObject.Find("AsteroidManager");
        shipHolder = GameObject.Find("SpaceshipSprite");
        asteroidlist = asteroidHolder.GetComponent<AsteroidMovement>().asteroids;
        ship = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (asteroidHolder.GetComponent<AsteroidMovement>().Asteroids.Count > 0)
        {
            asteroidlist = asteroidHolder.GetComponent<AsteroidMovement>().asteroids;
            foreach (GameObject o in asteroidlist)
            {

                currentAsteroid = o.GetComponent<SpriteRenderer>();
                float centersDistance = Mathf.Pow(ship.bounds.center.x - currentAsteroid.bounds.center.x, 2) + Mathf.Pow(ship.bounds.center.y - currentAsteroid.bounds.center.y, 2);
                Vector3 shipRadius = ship.bounds.max - ship.bounds.center;
                Vector3 currentAsteroidRadius = currentAsteroid.bounds.max - currentAsteroid.bounds.center;
                float radiiDistance = Mathf.Pow(shipRadius.magnitude + currentAsteroidRadius.magnitude, 2);
                if (centersDistance < radiiDistance)
                {
                    shipAsteroidEffect.Play();
                    originSetter = new Vector3(0, 0, 0);
                    shipHolder.GetComponent<ShipMovement>().ShipPosition = originSetter;
                    lives--;
                    asteroidToDestroy = o;
                    break;
                }

            }
            if (asteroidToDestroy != null)
            {
                asteroidHolder.GetComponent<AsteroidMovement>().SplitAsteroids(asteroidToDestroy, false);
                asteroidToDestroy = null;
            }
        }
        if (asteroidHolder.GetComponent<AsteroidMovement>().SmallerAsteroids.Count > 0)
        {

            smallerAsteroidList = asteroidHolder.GetComponent<AsteroidMovement>().SmallerAsteroids;
            foreach (GameObject o in smallerAsteroidList)
            {

                currentAsteroid = o.GetComponent<SpriteRenderer>();
                float centersDistance = Mathf.Pow(ship.bounds.center.x - currentAsteroid.bounds.center.x, 2) + Mathf.Pow(ship.bounds.center.y - currentAsteroid.bounds.center.y, 2);
                Vector3 shipRadius = ship.bounds.max - ship.bounds.center;
                Vector3 currentAsteroidRadius = currentAsteroid.bounds.max - currentAsteroid.bounds.center;
                float radiiDistance = Mathf.Pow(shipRadius.magnitude + currentAsteroidRadius.magnitude, 2);
                if (centersDistance < radiiDistance)
                {
                    shipAsteroidEffect.Play();
                    shipHolder.GetComponent<ShipMovement>().ShipPosition = originSetter;
                    lives--;
                    asteroidToDestroy = o;
                    break;
                }

            }
            if (asteroidToDestroy != null)
            {
                asteroidHolder.GetComponent<AsteroidMovement>().SplitAsteroids(asteroidToDestroy, true);
                asteroidToDestroy = null;
            }
        }
        livesPrint.text = "Lives: " + lives.ToString();
        if(lives == 0)
        {
            gameOverLoader.GetComponent<GameOver>().LoadOnce = true;
        }
    }
    
    

}

