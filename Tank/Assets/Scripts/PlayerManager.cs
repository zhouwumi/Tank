using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour {

    private static PlayerManager _instance;

    public bool IsEnd = false;
    public bool isDead;
    public Vector3 lastPlayerPosition;
    public Quaternion lastPlayerQuaternion;

    public int lifeValue;

    public GameObject bornPrefab;

    public GameObject txtPlayerScore;
    public GameObject txtPlayerLife;
    public GameObject imgOver;

    public int playerScore;

    public AudioClip dieAudioClip;

    private GameObject _heart;

    public static PlayerManager Instance
    {
        get
        {
            return _instance;
        }
        set
        {
            _instance = value;
        }
    }

    private void Awake()
    {
        Instance = this;
    }
    // Use this for initialization
    void Start () {
        txtPlayerScore.GetComponent<Text>().text = playerScore.ToString();
        txtPlayerLife.GetComponent<Text>().text = lifeValue.ToString();
        imgOver.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        if (IsEnd)
        {
            return;
        }
		if(isDead)
        {
            Recover();
        }
	}

    public void AddPlayerScore()
    {
        playerScore++;
        txtPlayerScore.GetComponent<Text>().text = playerScore.ToString();
    }

    void Recover()
    {
        if(lifeValue <= 0)
        {
            GameOver();
        }
        else
        {
            lifeValue--;
            txtPlayerLife.GetComponent<Text>().text = lifeValue.ToString();
            GameObject bornObject = Instantiate(bornPrefab, lastPlayerPosition, lastPlayerQuaternion);
            bornObject.GetComponent<Born>().isCreatePlayer = true;
            isDead = false;
            print("######生成一个玩家###");
        }
    }

    public void SetHeart(GameObject heart)
    {
        _heart = heart;
    }

    public void GameOver()
    {
        IsEnd = true;
        imgOver.SetActive(true);
        //AudioSource.PlayClipAtPoint(dieAudioClip, _heart.transform.position, 10);
        AudioSource.PlayClipAtPoint(dieAudioClip, _heart.transform.position, 10);
        Invoke("backToMenuScene", 2);
    }

    private void backToMenuScene()
    {
        SceneManager.LoadScene(0);
    }
}
