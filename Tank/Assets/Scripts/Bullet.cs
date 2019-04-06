using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public Sprite[] bulletSprites;
    private SpriteRenderer spriteRender;
    private Const.Direction _direction = Const.Direction.TOP;

    public float moveSpeed;

    public bool isMe;

    public AudioClip bulletAudioClip;

    private void Awake()
    {
        spriteRender = GetComponent<SpriteRenderer>();   
    }

    // Use this for initialization
    void Start () {
		
	}

    public Const.Direction getDirection()
    {
        return _direction;
    }
	
    public void SetDirection(Const.Direction newDirection)
    {
        _direction = newDirection;
        if(_direction == Const.Direction.TOP)
        {
            this.spriteRender.sprite = this.bulletSprites[0];
        }else if(_direction == Const.Direction.RIGHT)
        {
            this.spriteRender.sprite = this.bulletSprites[1];
        }else if(_direction == Const.Direction.DOWN)
        {
            this.spriteRender.sprite = this.bulletSprites[2];
        }else if(_direction == Const.Direction.LEFT)
        {
            this.spriteRender.sprite = this.bulletSprites[3];
        }
    }

	// Update is called once per frame
	void Update () {
		
	}

    //第一种方式  子弹的运动
    //private void FixedUpdate()
    //{
    //    if (_direction == Const.Direction.TOP)
    //    {
    //        transform.Translate(0, moveSpeed * Time.fixedDeltaTime, 0);
    //    }
    //    else if (_direction == Const.Direction.RIGHT)
    //    {
    //        transform.Translate(moveSpeed * Time.fixedDeltaTime, 0, 0);
    //    }
    //    else if (_direction == Const.Direction.DOWN)
    //    {
    //        transform.Translate(0, moveSpeed * Time.fixedDeltaTime * -1, 0);
    //    }
    //    else if (_direction == Const.Direction.LEFT)
    //    {
    //        transform.Translate(moveSpeed * Time.fixedDeltaTime * -1, 0, 0);
    //    }
    //}

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        transform.Translate(transform.up * this.moveSpeed * Time.fixedDeltaTime, Space.World);
    }

    public void PlayAudio()
    {
        AudioSource.PlayClipAtPoint(bulletAudioClip, transform.position);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        string tag = collision.gameObject.tag;
        switch (tag)
        {
            case "wall":
                Destroy(gameObject);
                Destroy(collision.gameObject);
                break;
            case "board_wall":
                Destroy(gameObject);
                break;
            case "river":
                break;
            case "heart":
                Destroy(gameObject);
                collision.SendMessage("Die");
                break;
            case "barriar":
                Destroy(gameObject);
                break;
            case "player":
                if (!this.isMe)
                {
                    Destroy(gameObject);
                    collision.SendMessage("Die");
                }
                break;
            case "enemy":
                if (this.isMe)
                {
                    PlayerManager.Instance.AddPlayerScore();
                    Destroy(gameObject);
                    Destroy(collision.gameObject);
                }
                break;
        }
    }
}
