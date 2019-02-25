using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingScript : MonoBehaviour
{

    // Use this for initialization
    public GameObject shootingObject;
    public AudioClip bulletLeavingEffect;
    public AudioSource musicSource;
    public GameObject bullet;
    private GameObject shotbullet;
    private List<Vector3> velocity;
    private List<GameObject> bullets;
    private List<Vector3> matchingDirections;
    private Vector3 currentPosition;
    private Vector3 currentBulletDirection;
    private Vector3 currentVelocity;
    Vector3 acceleration;

    float test;

    float accelerationRate = 1.1f;
    float maxspeed;
    void Start()
    {
        musicSource.clip = bulletLeavingEffect;
        currentVelocity = new Vector3(0, 0, 0);
        bullets = new List<GameObject>();
        velocity = new List<Vector3>();
        matchingDirections = new List<Vector3>();
        maxspeed = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (bullets.Count > 4)
            {

            }
            else
            {
                musicSource.Play();
                shotbullet = Instantiate(bullet, shootingObject.transform.position, Quaternion.identity);
                currentBulletDirection = new Vector3(shootingObject.GetComponent<ShipMovement>().Direction.x, shootingObject.GetComponent<ShipMovement>().Direction.y, shootingObject.GetComponent<ShipMovement>().Direction.z);
                velocity.Add(currentVelocity);
                matchingDirections.Add(currentBulletDirection);
                bullets.Add(shotbullet);
                
            }

        }

        if (bullets.Count > 0)
        {
            for (int i = 0; i < bullets.Count; i++)
            {
                acceleration = matchingDirections[i] * accelerationRate;
                velocity[i] += acceleration;
                velocity[i] = Vector3.ClampMagnitude(velocity[i], maxspeed);
                currentPosition = bullets[i].transform.position + velocity[i];
                bullets[i].transform.position = currentPosition;
            }

        }



    }
    public void BulletDestroyer(GameObject a)
    {
        int b = bullets.IndexOf(a);
        velocity.RemoveAt(b);
        matchingDirections.RemoveAt(b);
        bullets.RemoveAt(b);
        Destroy(a);
    }
    public List<GameObject> Bullets
    {
        get { return bullets; }
    }
    public List<Vector3> BulletDirections
    {
        get { return matchingDirections; }
    }
    public List<Vector3> BulletVelocities
    {
        get { return velocity; }
    }
}
