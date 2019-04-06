using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Heart : MonoBehaviour {

    public Sprite DieSprite;
    public GameObject explosionPrefab;

    private bool _hasDie;
    private SpriteRenderer _spriteRender;


	// Use this for initialization
	void Start () {
        _spriteRender = GetComponent<SpriteRenderer>();
        _hasDie = false;
        PlayerManager.Instance.SetHeart(gameObject);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Die()
    {
        if (_hasDie)
        {
            return;
        }
        _hasDie = true;
        _spriteRender.sprite = DieSprite;
        PlayerManager.Instance.GameOver();
    }
}
