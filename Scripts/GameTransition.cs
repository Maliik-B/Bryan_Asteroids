using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameTransition : MonoBehaviour {

    // Use this for initialization
    private GameObject lifesetter;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        {
            if (Input.GetKeyUp(KeyCode.Space))
            {
                SceneManager.LoadScene("SampleScene");
            }
        }
    }
}
