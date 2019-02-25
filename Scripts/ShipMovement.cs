using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMovement : MonoBehaviour {

    // Use this for initialization
    public float speed;
    public Vector3 vehiclePosition;
    public Vector3 direction;
    public Vector3 velocity;
    public float angleOfRotation;
    public float maxspeed;
    public float decelaration;
    public Vector3 acceleration;
    public float accelerationRate;
    float leftConstraint;
    float rightConstraint;
    float bottomConstraint;
    float topContraint;
    float distanceZ;
    Camera cam;
    Vector3 viewportP;
    void Start()
    {
        vehiclePosition = new Vector3(0, 0, 0);
        direction = new Vector3(1, 0, 0);
        velocity = new Vector3(0, 0, 0);
        acceleration = new Vector3(0, 0, 0);
        accelerationRate = 0.001f;
        maxspeed = 1f;
        decelaration = 0.9f;

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
    }
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {

            direction = Quaternion.Euler(0, 0, -1) * direction;
            angleOfRotation -= 1;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {

            direction = Quaternion.Euler(0, 0, 1) * direction;
            angleOfRotation += 1;
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            //velocity = direction * speed;

            acceleration = direction * accelerationRate;
            velocity += acceleration;
            velocity = Vector3.ClampMagnitude(velocity, maxspeed);
            vehiclePosition += velocity;
            transform.position = vehiclePosition;
        }
        else
        {
            velocity = velocity * decelaration;
            if (velocity.z < 0.2)
            {
                velocity.z = 0;
                vehiclePosition += velocity;
                transform.position = vehiclePosition;
            }
        }
        //maxspeed working as buffer
        if (transform.position.x < leftConstraint - 2)
        {
            vehiclePosition = new Vector3(rightConstraint - 1, transform.position.y, transform.position.z);
            transform.position = vehiclePosition;
        }
        if (transform.position.x > rightConstraint + 2)
        {
            vehiclePosition = new Vector3(leftConstraint + 1, transform.position.y, transform.position.z);
            transform.position = vehiclePosition;
        }
        if (transform.position.y < bottomConstraint - 2)
        {
            vehiclePosition = new Vector3(transform.position.x, topContraint - 1, transform.position.z);
            transform.position = vehiclePosition;
        }
        if (transform.position.y > topContraint + 2)
        {
            vehiclePosition = new Vector3(transform.position.x, bottomConstraint + 1, transform.position.z);
            transform.position = vehiclePosition;
        }
        transform.rotation = Quaternion.Euler(0, 0, angleOfRotation);
    }
    public Vector3 Direction
    {
        get { return direction; }
    }
    public Vector3 ShipPosition
    {
        get { return transform.position; }
        set { vehiclePosition = new Vector3(value.x,value.y,value.z);
            transform.position = vehiclePosition;
        }
    }
}
