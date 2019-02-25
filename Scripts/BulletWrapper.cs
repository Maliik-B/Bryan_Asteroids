using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletWrapper : MonoBehaviour {

    // Use this for initialization
    float leftConstraint;
    float rightConstraint;
    float bottomConstraint;
    float topContraint;
    float distanceZ;
    public GameObject bulletHolder;
    Vector3 bulletposition;
    float timer;
    Camera cam;
    Vector3 viewportP;
    void Start()
    {

        bulletHolder = GameObject.Find("BulletManager");
        cam = Camera.main;
        leftConstraint = Screen.width;
        rightConstraint = Screen.width;
        topContraint = Screen.height;
        bottomConstraint = Screen.height;
        distanceZ = Mathf.Abs(cam.transform.position.z + transform.position.z);
        leftConstraint = cam.ScreenToWorldPoint(new Vector3(0f, 0f, distanceZ)).x;
        rightConstraint = cam.ScreenToWorldPoint(new Vector3(Screen.width, 0f, distanceZ)).x;
        bottomConstraint = cam.ScreenToWorldPoint(new Vector3(0f, 0f, distanceZ)).y;
        topContraint = cam.ScreenToWorldPoint(new Vector3(0f, Screen.height, distanceZ)).y;
        timer = Time.realtimeSinceStartup;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < leftConstraint - 2)
        {
            bulletposition = new Vector3(rightConstraint - 1, transform.position.y, transform.position.z);
            transform.position = bulletposition;
        }
        if (transform.position.x > rightConstraint + 2)
        {
            bulletposition = new Vector3(leftConstraint + 1, transform.position.y, transform.position.z);
            transform.position = bulletposition;
        }
        if (transform.position.y < bottomConstraint - 2)
        {
            bulletposition = new Vector3(transform.position.x, topContraint - 1, transform.position.z);
            transform.position = bulletposition;
        }
        if (transform.position.y > topContraint + 2)
        {
            bulletposition = new Vector3(transform.position.x, bottomConstraint + 1, transform.position.z);
            transform.position = bulletposition;
        }
        if(timer + .5f <= Time.realtimeSinceStartup)
        {

            bulletHolder.GetComponent<ShootingScript>().BulletDestroyer(gameObject);
        }
    }
    
}
