﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;      
using UnityEngine;

public class MainMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            SceneManager.LoadScene("MainMenu");
        }
	}
}
