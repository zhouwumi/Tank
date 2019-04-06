using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Born : MonoBehaviour {

    public GameObject playerPrefab;
    public GameObject[] enemyPrefabs;
    public bool isCreatePlayer;

	// Use this for initialization
	void Start () {
        Invoke("GeneratePlayer", 1.0f);
        Destroy(gameObject, 1.0f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void GeneratePlayer()
    {
        if(isCreatePlayer)
        {
            Instantiate(playerPrefab, transform.position, transform.rotation);
        }
        else
        {
            int idx = Random.Range(0, enemyPrefabs.Length - 1);
            GameObject enemyPrefab = enemyPrefabs[idx];
            Instantiate(enemyPrefab, transform.position, transform.rotation);
        }
    }
}
