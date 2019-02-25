using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverScript : MonoBehaviour {

    // Use this for initialization
    public AudioSource shipAsteroidEffect;
    void Start () {
        shipAsteroidEffect.Play(); ;
}
	
	// Update is called once per frame
	void Update () {
		
	}
}
