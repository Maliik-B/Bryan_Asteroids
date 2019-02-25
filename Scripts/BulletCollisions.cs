using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCollisions : MonoBehaviour {

    // Use this for initialization
    SpriteRenderer bullet;
    SpriteRenderer currentAsteroid;
    private AudioSource musicSource;
    private GameObject BulletCollisionSoundGO;
    GameObject asteroidScript;
    GameObject asteroidToDestroy;
    List<GameObject> asteroidlist;
    List<GameObject> smallerAsteroidList;
    List<GameObject> bullets;
    private GameObject asteroidHolder;
    public GameObject bulletHolder;
    public int count;
    void Start()
    {
        BulletCollisionSoundGO = GameObject.Find("BulletCollisionSound ");
        musicSource = BulletCollisionSoundGO.GetComponent<AudioSource>();
        asteroidToDestroy = null;
        asteroidHolder = GameObject.Find("AsteroidManager");
        bulletHolder = GameObject.Find("BulletManager");
        bullet = gameObject.GetComponent<SpriteRenderer>();
        
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 test = bullet.bounds.max;
        bullet = gameObject.GetComponent<SpriteRenderer>();
        if (asteroidHolder.GetComponent<AsteroidMovement>().asteroids.Count > 0)
        {
            asteroidlist = asteroidHolder.GetComponent<AsteroidMovement>().asteroids;
            foreach (GameObject o in asteroidlist)
            {
                
                currentAsteroid = o.GetComponent<SpriteRenderer>();
                float centersDistance = Mathf.Pow(bullet.bounds.center.x - currentAsteroid.bounds.center.x, 2) + Mathf.Pow(bullet.bounds.center.y - currentAsteroid.bounds.center.y, 2);
                Vector3 bulletRadius = bullet.bounds.max - bullet.bounds.center;
                Vector3 currentAsteroidRadius = currentAsteroid.bounds.max - currentAsteroid.bounds.center;
                float radiiDistance = Mathf.Pow(bulletRadius.magnitude + currentAsteroidRadius.magnitude, 2);
                if (centersDistance < radiiDistance)
                {
                    musicSource.Play();
                    bulletHolder.GetComponent<ShootingScript>().BulletDestroyer(gameObject);
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
        if (asteroidHolder.GetComponent<AsteroidMovement>().smallerAsteroids.Count > 0)
        {

            smallerAsteroidList = asteroidHolder.GetComponent<AsteroidMovement>().smallerAsteroids;
            foreach (GameObject o in smallerAsteroidList)
            {

                currentAsteroid = o.GetComponent<SpriteRenderer>();
                float centersDistance = Mathf.Pow(bullet.bounds.center.x - currentAsteroid.bounds.center.x, 2) + Mathf.Pow(bullet.bounds.center.y - currentAsteroid.bounds.center.y, 2);
                Vector3 bulletRadius = bullet.bounds.max - bullet.bounds.center;
                Vector3 currentAsteroidRadius = currentAsteroid.bounds.max - currentAsteroid.bounds.center;
                float radiiDistance = Mathf.Pow(bulletRadius.magnitude + currentAsteroidRadius.magnitude, 2);
                if (centersDistance < radiiDistance)
                {
                    musicSource.Play();
                    bulletHolder.GetComponent<ShootingScript>().BulletDestroyer(gameObject);
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
        
        
    }
}
