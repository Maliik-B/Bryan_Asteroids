using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameOver : MonoBehaviour {

    // Use this for initialization
    public GameObject lifeGrabber;
    private bool loadOnce;
    private int lives;
	void Start () {
        loadOnce = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (loadOnce)
        {
            if (lifeGrabber.GetComponent<ShipChecking>().count == 0)
            {
               
                SceneManager.LoadScene("GameOverScene");
            }
            loadOnce = false;
        }
		
	}
    public bool LoadOnce
    {
        set { loadOnce = value; }
    }
}
