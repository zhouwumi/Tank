using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Option : MonoBehaviour {

    public int playerIdx;
    public Transform pos1;
    public Transform pos2;

	// Use this for initialization
	void Start () {
        playerIdx = 0;
        transform.position = pos1.position;
    }
	
	// Update is called once per frame
	void Update () {
	    if(Input.GetKeyDown(KeyCode.W))
        {
            if(playerIdx == 0)
            {
                return;
            }
            playerIdx = 0;
            transform.position = pos1.position;
            return;
        }

        if(Input.GetKeyDown(KeyCode.S))
        {
            if(playerIdx == 1)
            {
                return;
            }
            playerIdx = 1;
            transform.position = pos2.position;
        }

        if(playerIdx == 0 && Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(1);
        }
	}
}
