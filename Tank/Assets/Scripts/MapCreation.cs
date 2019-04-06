using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCreation : MonoBehaviour {

    public float halfHeight;
    public float halfWidth;

    //0:墙, 1:铁窗, 2:草, 3:河流
    public GameObject[] itemPrefabs;
    public GameObject heartPrefab;
    public GameObject bornPrefab;

    public List<Vector3> allUsedPositions;

	// Use this for initialization
	void Start () {
        halfHeight = 8f;
        halfWidth = 10.17f;

        this.CreateMapHeart();
        this.GenerateAllItems();
        this.CreatePlayers();
    }

    private void CreateMapHeart()
    {
        _createPrefab(heartPrefab, new Vector3(0, halfHeight * -1, 0), Quaternion.identity);

        _createPrefab(itemPrefabs[0], new Vector3(0, halfHeight * -1 + 1, 0), Quaternion.identity);
        _createPrefab(itemPrefabs[0], new Vector3(-1, halfHeight * -1 + 1, 0), Quaternion.identity);
        _createPrefab(itemPrefabs[0], new Vector3(-1, halfHeight * -1, 0), Quaternion.identity);
        _createPrefab(itemPrefabs[0], new Vector3(1, halfHeight * -1 + 1, 0), Quaternion.identity);
        _createPrefab(itemPrefabs[0], new Vector3(1, halfHeight * -1, 0), Quaternion.identity);
    }

    private void CreatePlayers()
    {
        GameObject playerObject = _createPrefab(bornPrefab, _getNextPosition(), Quaternion.identity);
        playerObject.GetComponent<Born>().isCreatePlayer = true;

        for(int i = 0; i < 10; i++)
        {
            GameObject enemyObject = _createPrefab(bornPrefab, _getNextPosition(), Quaternion.identity);
            enemyObject.GetComponent<Born>().isCreatePlayer = false;
        }
    }
	
    private GameObject _createPrefab(GameObject prefabObject, Vector3 position, Quaternion quaternion)
    {
        this.allUsedPositions.Add(position);
        GameObject returnObject = Instantiate(prefabObject, position, quaternion);
        returnObject.transform.SetParent(transform);
        return returnObject;
    }

    private void GenerateAllItems()
    {
        for(int i = 0; i < 80; i++)
        {
            _generateNextItem();
        }
    }

    private GameObject _generateNextItem()
    {
        int randomPrefabIdx = Random.Range(0, itemPrefabs.Length);
        return _createPrefab(itemPrefabs[randomPrefabIdx], _getNextPosition(), Quaternion.identity);
    }

    private Vector3 _getNextPosition()
    {
        while (true)
        {
            int randomX = Mathf.CeilToInt(Random.Range(-1 * (halfWidth - 1), 1 * (halfWidth - 1)));
            int randomY = Mathf.CeilToInt(Random.Range(-1 * (halfHeight - 1), 1 * (halfHeight - 1)));
            Vector3 nextPosition = new Vector3(randomX, randomY, 0);
            if (!this._hasPosition(nextPosition))
            {
                return nextPosition;
            }
        }
    }

    private bool _hasPosition(Vector3 position)
    {
        foreach( Vector3 itemPosition in allUsedPositions)
        {
            if(itemPosition == position)
            {
                return true;
            }
        }
        return false;
    }

	// Update is called once per frame
	void Update () {
		
	}
}
